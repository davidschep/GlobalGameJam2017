using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace GlobalGamejam
{
    public class MinigameManager
    {
        public delegate void VoidDelegate();
        public event VoidDelegate OnMinigameFailed;
        public event VoidDelegate OnMinigameSuccess;
        // the key to switch the numbers to the left
        private const KeyCode m_switchLeft = KeyCode.LeftArrow;
        // the key to switch the numbers to the right
        private const KeyCode m_switchRight = KeyCode.RightArrow;
        // the resource path to instantiate the root prefab
        private const string m_rootPath = "Minigame/Prefabs/Root";
        // the const minigame grid variables
        private const int m_grid_columns = 10;
        private const int m_grid_rows = 4;
        // the const row length
        private const int m_row_length = 10;
        // the amount of clock digits
        private const int m_clock_digits = 2;
        // the difficulty value
        private int m_difficulty;
        // the remaining time
        private float m_timeLeft;
        // the visual time objects
        private Text[] m_clock;
        // the char grid
        private int[][] m_grid;
        // the visible text grid
        private Text[][] m_textGrid;
        // the number row
        private int[] m_row;
        // the visible text row
        private Text[] m_textRow;
        // win or lose value
        private bool m_succeeded;
        // the selected row to switch numbers
        private int m_selectedRow;
        private GameObject m_hidden;

        // load everything to prevent framedrops later
        public void Initialize()
        {
            // set succeeded to false
            m_succeeded = false;
            // load and instantiate the root
            GameObject root = GameObject.Instantiate(Resources.Load<GameObject>(m_rootPath));
            // set the amount of rows of both grids
            m_grid = new int[m_grid_rows][];
            m_textGrid = new Text[m_grid_rows][];
            // set the cover object
            m_hidden = root.transform.FindChild("CoveredCenter").gameObject;
            // fill the grids
            for (int yi = 0; yi < m_grid_rows; yi++)
            {
                // set the amount of columns of both grids
                m_grid[yi] = new int[m_grid_columns];
                m_textGrid[yi] = new Text[m_grid_columns];

                for (int xi = 0; xi < m_grid_columns; xi++)
                {
                    // set the values of the grid elements
                    m_grid[yi][xi] = '*';
                    m_textGrid[yi][xi] = root.transform.FindChild("Grid").FindChild(yi.ToString()).transform.FindChild(xi.ToString()).FindChild("LABEL").FindChild("Text").GetComponent<Text>();
                }
            }
            // set the size of both rows
            m_row = new int[m_row_length];
            m_textRow = new Text[m_row_length];
            // fill the text row
            for (int i = 0; i < m_row_length; i++)
            {
                m_textRow[i] = root.transform.FindChild("NumberBar").FindChild(i.ToString()).FindChild("Text").GetComponent<Text>();
            }

            // set the clock values
            m_clock = new Text[m_clock_digits];
            for (int i = 0; i < m_clock_digits; i++)
            {
                m_clock[i] = root.transform.FindChild("Counter").FindChild(i.ToString()).FindChild("Text").GetComponent<Text>();
            }
            SetNumbers();
        }

        public void StartMinigame(GameManager manager, int difficulty)
        {
            m_difficulty = difficulty;
            m_timeLeft = 10 + difficulty; // <- calculate a good value with the difficulty
            SetNumbers();
            // process input until solved or out of time
            manager.StartCoroutine(MinigameUpdate());
        }

        // generate all numbers
        private void SetNumbers()
        {
            m_hidden.SetActive(true);
            for (int yi = 0; yi < m_grid_rows; yi++)
            {
                RandomizeRow(m_grid[yi], m_textGrid[yi], m_grid_columns);
            }
            RandomizeRow(m_row, m_textRow, m_row_length);
        }

        // randomizes the row with values 0-10
        private void RandomizeRow(int[] row, Text[] textRow, int length)
        {
            int value;
            // create a list to keep track of the taken numbers
            List<int> takenNumbers = new List<int>();
            // while not every values
            while (takenNumbers.Count < length)
            {
                // get a value from 0 - 10 in a random order
                do value = Random.Range(0, m_grid_columns);
                while (takenNumbers.Contains(value));
                // set the value
                row[takenNumbers.Count] = value;
                // update the text element
                textRow[takenNumbers.Count].text = value.ToString();
                // add the value to the taken numbers
                takenNumbers.Add(value);
            }
        }

        private void UpdateTextGrid()
        {
            for (int yi = 0; yi < m_grid_rows; yi++)
            {
                for (int xi = 0; xi < m_grid_columns; xi++)
                {
                    m_textGrid[yi][xi].text = m_grid[yi][xi].ToString();
                }
            }
        }

        private IEnumerator MinigameUpdate()
        {
            while (!m_succeeded && m_timeLeft > 0)
            {
                if (Input.GetKeyDown(m_switchLeft))
                {
                    SwitchNumbers(1);
                }
                if (Input.GetKeyDown(m_switchRight))
                {
                    SwitchNumbers(-1);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    m_selectedRow--;
                    if (m_selectedRow < 0) m_selectedRow += m_grid_rows;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    m_selectedRow++;
                    if (m_selectedRow < 0) m_selectedRow -= m_grid_rows;
                }
                m_timeLeft -= Time.deltaTime;
                SetVisualTime(m_timeLeft);
                yield return null;
            }
            if (m_succeeded && OnMinigameSuccess != null) OnMinigameSuccess();
            else if (OnMinigameFailed != null) OnMinigameFailed();
            yield break;
        }

        private void SetVisualTime(float time)
        {
            time = Mathf.Round(time);
            m_clock[0].text = (time - Mathf.Floor(time / 10) * 10).ToString()[0].ToString();
            m_clock[1].text = Mathf.Floor(time / 10).ToString()[0].ToString();
        }

        private void SwitchNumbers(int dir)
        {
            //for (int yi = 0; yi < m_grid_rows; yi++)
            //{
            int[] tempRow = new int[m_grid_columns];
            Array.Copy(m_grid[m_selectedRow], tempRow, m_grid_columns);

            for (int xi = 0; xi < m_grid_columns; xi++)
            {
                // calculate the new index
                int newColumnIndex;
                if (dir > 0) newColumnIndex = (xi + 1 /*(m_selectedRow + 1)*/) % m_grid_columns;
                else newColumnIndex = (xi - 1/*(m_selectedRow + 1)*/) % m_grid_columns;
                if (newColumnIndex < 0) newColumnIndex += m_grid_columns;
                m_grid[m_selectedRow][xi] = tempRow[newColumnIndex];
                m_textGrid[m_selectedRow][xi].text = m_grid[m_selectedRow][xi].ToString();
            }
            //}
            Validate();
        }

        private void Validate()
        {
            for (int xi = 0; xi < m_grid_columns; xi++)
            {
                int num = m_grid[0][xi];
                int count = 0;
                for (int yi = 0; yi < m_grid_rows; yi++)
                {
                    if (m_grid[yi][xi] == num) count++;
                }
                if (count == m_grid_rows)
                {
                    if (m_row[4] == num || m_row[5] == num)
                    {
                        m_hidden.SetActive(false);
                        m_succeeded = true;
                    }
                }
            }
        }
    }
}

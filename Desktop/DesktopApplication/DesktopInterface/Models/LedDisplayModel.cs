/**
 ******************************************************************************
 * @file    LED Display Control Example/Model/LedDisplayModel.cs
 * @author  Adrian Wojcik  adrian.wojcik@put.poznan.pl
 * @version V2.0
 * @date    10-May-2021
 * @brief   LED display controller: LED display data model - matrix of LEDs
 ******************************************************************************
 */

using DesktopInterface.Control;
using DesktopInterface.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DesktopInterface.Models
{
    public class LedDisplayModel
    {
        public readonly int SizeX = 8;  //!< Display horizontal size
        public readonly int SizeY = 8;  //!< Display vertical size
        private Led[,] _model;     //!< Display data model - matrix of LEDs

        /**
         * @brief Default constructor
         */
        public LedDisplayModel()
        {
            _model = new Led[SizeX, SizeY];
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    _model[x, y] = new Led();
        }

        /**
         * @brief Class indexer - access to model with '[]' operator
         * @param i First index of model container
         * @param j Second index of model container
         */
        public Led this[int i, int j]
        {
            get { return _model[i, j]; }
            set { _model[i, j] = value; }
        }

        /**
         * Conversion method: LED x-y position to position/color data to Dto
         * @param x LED horizontal position in display
         * @param y LED vertical position in display
         * @return Position/color data in LedDto format
         */
        private LedDto GetLedDto(int x, int y)
        {
            LedDto led = new LedDto(x, y);
            try
            {
                led.x = x;
                led.y = y;
                led.R = _model[x, y].R;
                led.B = _model[x, y].B;
                led.G = _model[x, y].G;
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
            return led;
        }

        /**
         * @brief Generate HTTP POST request parameters for LED display control via IoT server script
         * @return HTTP POST request parameters as Key-Value pairs
         */
        public List<LedDto> GetControlPostData()
        {
            var postData = new List<LedDto>();
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    if (_model[i, j].ColorNotNull())
                        postData.Add(GetLedDto(i, j));
                        //postData.Add(
                        //    new KeyValuePair<string, string>(
                        //        "LED" + i.ToString() + j.ToString(),
                        //        IndexToJsonArray(i, j).ToString()
                        //        ));
                }
            }
            return postData;
        }

        /**
         * @brief Generate HTTP POST request parameters for clearing LED display via IoT server script
         * @return HTTP POST request parameters as Key-Value pairs
         */
        List<LedDto>? clearData;
        public List<LedDto> GetClearPostData()
        {
            if (clearData == null)
            {
                clearData = new List<LedDto>();
                for (int i = 0; i < SizeX; i++)
                {
                    for (int j = 0; j < SizeY; j++)
                    {
                        clearData.Add(new LedDto(i, j));
                    }
                }
            }
            return clearData;
        }
    }
}
﻿using Caliburn.Micro;
using DesktopInterface.Control;
using DesktopInterface.Dtos;
using DesktopInterface.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopInterface.ViewModels
{
    public class LedControlViewModel : Screen, IConductorExtension
    {
        private List<List<LedViewModel>> _leds;

        private int _r;

        private int _g;

        private int _b;

        private readonly int _displaySizeX;

        private readonly int _displaySizeY;

        private SolidColorBrush _previewColor;

        public int R { get { return _r; } set { _r = value; ModelToBrush(); NotifyOfPropertyChange(() => R); } }

        public int G { get { return _g; } set { _g = value; ModelToBrush(); NotifyOfPropertyChange(() => G); } }

        public int B { get { return _b; } set { _b = value; ModelToBrush(); NotifyOfPropertyChange(() => B); } }

        public int DisplaySizeX { get { return _displaySizeX; } }

        public int DisplaySizeY { get { return _displaySizeY; } }

        public SolidColorBrush PreviewColor { get { return _previewColor; } set { _previewColor = value; NotifyOfPropertyChange(() => PreviewColor); } }

        public Grid ButtonMatrixGrid { get; set; }

        public List<List<LedViewModel>> Leds { get { return _leds; } set { _leds = value; NotifyOfPropertyChange(() => Leds); } }

        public List<List<ButtonCommand>> LedCommands { get; set; }

        public LedControlViewModel() 
        {
            _r = 0;
            _g = 0;
            _b = 0;
            _previewColor = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            _displaySizeX = 8;
            _displaySizeY = 8;
            ButtonMatrixGrid = new Grid();
            ButtonMatrixGrid.Visibility = Visibility.Visible;
            _leds = new List<List<LedViewModel>>();
            if (WindowViewModel.Leds!.Count == 64)
            {
                for (int x = 0; x < DisplaySizeX; x++)
                {
                    _leds.Add(new List<LedViewModel>());
                    for (int y = 0; y < DisplaySizeY; y++)
                    {
                        _leds[x].Add(new LedViewModel(WindowViewModel.Leds[x * 8 + y]));
                    }
                }
                LedCommands = new List<List<ButtonCommand>>();
                for (int x = 0; x < DisplaySizeX; x++)
                {
                    LedCommands.Add(new List<ButtonCommand>());
                    for (int y = 0; y < DisplaySizeY; y++)
                    {
                        var led = _leds[x][y];
                        LedCommands[x].Add(
                            new ButtonCommand(
                                () => { led.SetViewColor(R, G, B); }));
                    }
                }
            }
        }

        public Binding GetCommandBinding(int x, int y)
        {
            return new Binding("LedCommands[" + x.ToString() + "][" + y.ToString() + "]");
        }

        public Binding GetColordBinding(int x, int y)
        {
            return new Binding("Leds[" + x.ToString() + "][" + y.ToString() + "].ViewColor");
        }

        public async void SendCommand()
        {
            _ = await ApiHelper.PostLeds(GetControlPostData());
        }

        public async void ClearCommand() 
        {
            // Clear display ViewModel
            for (int x = 0; x < DisplaySizeX; x++)
                for (int y = 0; y < DisplaySizeY; y++)
                    Leds[x][y].ClearViewColor();
            // Send request to clear device
            _ = await ApiHelper.PostLeds(GetClearPostData());
        }

        private void ModelToBrush()
        {
            PreviewColor = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
        }

        private List<LedDto> GetControlPostData()
        {
            var postData = new List<LedDto>();
            for (int i = 0; i < DisplaySizeX; i++)
            {
                for (int j = 0; j < DisplaySizeY; j++)
                {
                    if (Leds[i][j]._model.ColorNotNull())
                        postData.Add(new LedDto(Leds[i][j]._model));
                }
            }
            return postData;
        }

        public List<LedDto> GetClearPostData()
        {
            List<LedDto> clearData = new();
            for (int i = 0; i < DisplaySizeX; i++)
            {
                for (int j = 0; j < DisplaySizeY; j++)
                {
                    clearData.Add(new LedDto(i, j));
                }
            }

            return clearData;
        }

        public void DisposeOfContents()
        {
            // Intentionally left empty
        }
    }
}

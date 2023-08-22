﻿using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using NAudio.Wave;

namespace Esperanto
{
    public class MethodHelper
    {
        public bool isPhotoVisible = false; // Track photo visibility
        public bool isPhotoSoundVisible = false; // Track photo visibility
        public SoundPlayer soundPlayer;

        public void showPhoto(string path, Image popupImage)
        {
            popupImage.Visibility = Visibility.Visible;
            if (!isPhotoVisible)
            {
                BitmapImage imageSource = new BitmapImage(new Uri(path
                    ,
                    UriKind.RelativeOrAbsolute));

                popupImage.Source = imageSource;

                popupImage.Visibility = Visibility.Visible;
            }
            else
            {
                popupImage.Visibility = Visibility.Hidden;
            }

            isPhotoVisible = !isPhotoVisible;
        }

        public void doSound(string path)
        {
            var soundFilePath = path;
            if (File.Exists(soundFilePath))
            {
                using (var audioFile = new AudioFileReader(soundFilePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                    }
                }
            }
            else
            {
                MessageBox.Show("Sound file not found.");
            }
        }
    }
}
using Prism.Mvvm;
using System;
using System.ComponentModel;

namespace Ameba.Common.Model
{
    public class ImageMemViewer : BindableBase
    {
        public double TotalWidth { get; set; }
        public UInt32 TotalImages { get; set; }
        public UInt32 StartMemAddress { get; set; }
        public UInt32 TotalMemSize { get; set; }
        public bool[] ImageIsVisible { get; set; }
        public UInt32[] ImageStartPoint { get; set; }
        public UInt32[] ImageWidth { get; set; }


        double MemParrotWeight;

        private ImageMemViewer()
        {

        }

        public ImageMemViewer(UInt32 ImagesNum, UInt32 StartAddr, UInt32 MemSize)
        {
            TotalImages = ImagesNum;
            StartMemAddress = StartAddr;
            TotalMemSize = MemSize;

            ImageIsVisible = new bool[TotalImages];
            ImageStartPoint = new UInt32[TotalImages];
            ImageWidth = new UInt32[TotalImages];
        }

        public void UpdateImage(UInt32 image, UInt32 start, UInt32 size)
        {
            MemParrotWeight = (double)TotalWidth / (double)TotalMemSize;
            if (image < TotalImages)
            {
                if(start + size <= TotalMemSize)
                {
                    if(size > 0)
                    {
                        ImageIsVisible[image] = true;
                        ImageStartPoint[image] = (UInt32)(StartMemAddress + (double)start * MemParrotWeight);
                        ImageWidth[image] = (UInt32)((double)size * MemParrotWeight);
                    }
                    else
                    {
                        ImageIsVisible[image] = false;
                        ImageStartPoint[image] = 0x00;
                        ImageWidth[image] = 0xFF;
                    }
                }

                OnPropertyChanged(new PropertyChangedEventArgs("ImageIsVisible"));
                OnPropertyChanged(new PropertyChangedEventArgs("ImageStartPoint"));
                OnPropertyChanged(new PropertyChangedEventArgs("ImageWidth"));
            }
        }

        public void HideImage(UInt32 image)
        {
            if (image < TotalImages)
            {
                ImageIsVisible[image] = false;

                OnPropertyChanged(new PropertyChangedEventArgs("ImageIsVisible"));
            }
        }
    }
}

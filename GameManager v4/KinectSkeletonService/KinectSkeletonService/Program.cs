using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coding4Fun.Kinect.KinectService.Common;
using Coding4Fun.Kinect.KinectService.Listeners;
using Microsoft.Kinect;

namespace KinectSkeletonService
{
    class Program
    {
        SkeletonListener skeletonListener;
        static void Main(string[] args)
        {
            int socket;
            if (args.Length < 2)
            {
                Console.WriteLine("Needs a parameter for Streaming Socket");
                return;
            }
            else
            {
                socket = Convert.ToInt32(args[1]);
                if (socket < 0 || socket > 65535)
                {
                    Console.WriteLine("Socket must be in range 0 - 65535");
                    return;
                }
            }
            KinectSensor kinect = KinectSensor.KinectSensors[0];

            kinect.SkeletonStream.Enable();
            kinect.Start();

            SkeletonListener skeletonListener = new SkeletonListener(kinect, socket);
            skeletonListener.Start();
        }

        
    }
}

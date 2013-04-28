using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace KinectTracking
{
    class Kinect
    {
        // kinect variables
        KinectSensor kinectSensor;
        public Skeleton player;
        Skeleton[] playerData;
        public bool enabled;
        public Kinect()
        {
            enabled = false;
        }
        ~Kinect()
        {
            if(enabled)
            kinectSensor.Stop();
        }
        public void initialize( int elevationAngle = 0 )
        {

            try { kinectSensor = KinectSensor.KinectSensors[0]; }
            catch (Exception e)
            {
                Console.WriteLine("kinect not detected, continuing with kinect disabled {0}",e);
                return;
            }

            enabled = true;
            if (elevationAngle >= 26 )
            {
                elevationAngle = 26;
            }
            else if (elevationAngle <= -26)
            {
                elevationAngle = -26;
            }
            kinectSensor.SkeletonStream.Enable();
            kinectSensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinectSkeletonFrameReadyCallback);
            kinectSensor.Start();
            kinectSensor.ElevationAngle = elevationAngle;

        }


        void kinectSkeletonFrameReadyCallback(object sender, SkeletonFrameReadyEventArgs skeletonFrames)
        {
            using (SkeletonFrame skeleton = skeletonFrames.OpenSkeletonFrame())
            {
                if (skeleton != null)
                {
                    if (playerData == null || this.playerData.Length != skeleton.SkeletonArrayLength)
                    {
                        this.playerData = new Skeleton[skeleton.SkeletonArrayLength];
                    }
                    skeleton.CopySkeletonDataTo(playerData);
                }
            }

            if (playerData != null)
            {
                foreach (Skeleton skeleton in playerData)
                {
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        player = skeleton;
                    }
                }
            }
        }
    }
}

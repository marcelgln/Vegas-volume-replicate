//**************************************************************************************************
//! \file       EnvelopePoints.cs
//! \brief      Supporting functions for envelope point replication. 
//! \author     Marcel Gielen
//! \date       12-02-2023
//**************************************************************************************************

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ScriptPortal.Vegas;


namespace volEnvelopeReplicate
{
    public static class PointFuncs
    {
        //--------------------------------------------------------------------------------------------------
        //! \brief      Clone an area of envelope points from on track to another
        //! \details    The selected area of envelop points from dest will be an exact copy from src. 
        //!             Any existing envelope points are removed from dest.    
        //! \param[in]  point - List of source points
        //! \param[in]  point - List of destination points
        //! \param[in]  tStart - Timecode from area points are copied (point on tStart is included)
        //! \param[in]  tEnd - Timecode up-to and including where points are copied
        public static void ReplicatePoints(EnvelopePoints src, EnvelopePoints dst, Timecode tStart, Timecode tEnd)
        {
            // First delete any exiting points in the destination
            DeletePoints(dst, tStart, tEnd);
            foreach( EnvelopePoint srcPoint in src )
            {
                if( srcPoint.X >= tStart && srcPoint.X <= tEnd )
                {
                    EnvelopePoint p = new EnvelopePoint(srcPoint.X, srcPoint.Y, srcPoint.Curve);
                    dst.Add(p);
                }
            }
        }

        //--------------------------------------------------------------------------------------------------
        //! \brief      Delete all envelope points which are >= tStart and <= tEnd
        //! \param[in]  point - List of points
        //! \param[in]  tStart - Timecode from where points are deleted (point on tStart is included)
        //! \param[in]  tEnd - Timecode up-to and including points are deleted
        public static void DeletePoints(EnvelopePoints points, Timecode tStart, Timecode tEnd)
        {
            for (int i = points.Count - 1; i >= 0; i--)
            {
                if (points[i].X >= tStart && points[i].X <= tEnd)
                {
                    points.RemoveAt(i);
                }
            }
        }

    }

} // namaspace

//**************************************************************************************************
//! \file       GroupVolEnvelopeReplicate.cs
//! \brief      Vegas-Pro C# script to replicate volume envelope from selected events to all other 
//!             tracks within the same track group
//! \author     Marcel Gielen
//! \date       12-02-2023
//**************************************************************************************************

using ScriptPortal.Vegas;
using System.Collections.Generic;
using System.Windows.Forms;

namespace volEnvelopeReplicate
{
    public class EntryPoint
    {
        //--------------------------------------------------------------------------------------------------
        //! \brief      Entrypoint from Vegas
        //! \param[in]  Vegas instance
        public void FromVegas(Vegas vegas)
        {
            TrackVolEnvelope replicator = new TrackVolEnvelope(vegas);

            Track srcTrack = replicator.FindSourceTrack();
            if (srcTrack != null)
            {
                replicator.ReplicateVolumeEnvelopeSelection(srcTrack, replicator.GetDestTracks(srcTrack) );
            }
        }
    } 
}





//**************************************************************************************************
//! \file       TrackVolEnvelope.cs
//! \brief      Suporting functiocn for track volume envelope handing
//! \author     Marcel Gielen
//! \date       12-02-2023
//**************************************************************************************************

using System.Collections.Generic;
using System.Windows.Forms;
using ScriptPortal.Vegas;

namespace volEnvelopeReplicate
{
    public class TrackVolEnvelope
    {
        //--------------------------------------------------------------------------------------------------
        //! \brief      Constructor
        //! \param[in]  Vegas instance
        public TrackVolEnvelope(Vegas vegas)
        {
            m_vegas = vegas;
        }


        //--------------------------------------------------------------------------------------------------
        //! \brief      Replicate selected-events volume envelope from 'FromTrack' to all tracks in 'ToTracks'
        //! \details    The selected area of envelope points in the detination tracks will be an exact copy 
        //!             of the selected area in src. Any existing points in the same area in ToTracks are deleted.  
        //! \param[in]  fromTrack - Track from which colume envelope will be replicated.
        //! \param[in]  toTrack - List of tracks replicate the points to.  
        public void ReplicateVolumeEnvelopeSelection(Track fromTrack, List<Track> ToTracks)
        {
            foreach ( TrackEvent ev in  fromTrack.Events )
            {
                if( ev.Selected )
                {
                    Envelope srcEnv = fromTrack.Envelopes.FindByType(EnvelopeType.Volume);

                    foreach (Track destTrack in ToTracks)
                    {
                        Envelope destEnv = destTrack.Envelopes.FindByType(EnvelopeType.Volume);
                        PointFuncs.ReplicatePoints(srcEnv.Points, destEnv.Points, ev.Start, ev.Start + ev.Length);
                    }
                }
            }
        }


        //--------------------------------------------------------------------------------------------------
        //! \brief      Find source track for replicating volume envelope to other tracks. 
        //! \details    To determine the source track, only a single track may contain selected events, 
        //!             if this track is a member of a track group, then it is retrurned as the source track. 
        //! \return     Source track or null                             
        public Track FindSourceTrack()
        {
            const string ERROR_DLG_TITLE = "Selection error";
            Track srcTrack = null;
            bool NoEventSelected = true;

            foreach (Track track in m_vegas.Project.Tracks)
            {
                // loop through all events on the track, until a selected event is found
                foreach (TrackEvent trackEvent in track.Events)
                {
                    if (trackEvent.Selected)
                    {
                        if (srcTrack == null)
                        {
                            srcTrack = track;
                            NoEventSelected = false;
                        }
                        else
                        {
                            MessageBox.Show("Unable to determine source track for volume envelope copy when events on multiple tracks are selected",
                                ERROR_DLG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            srcTrack = null;
                            break;
                        }
                    }
                }
            }
            if (NoEventSelected)
            {
                MessageBox.Show("At least one event from a track group must be selected for volume envelope copy",
                    ERROR_DLG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (srcTrack != null)
            {
                if (GetTrackgroup(srcTrack) == null)
                {
                    MessageBox.Show("Track must be a member of a track group for volume envelope copy",
                        ERROR_DLG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    srcTrack = null;
                }

                if (srcTrack.Envelopes.FindByType(EnvelopeType.Volume) == null)
                {
                    MessageBox.Show("Source track for envelope copy must contain a volume envelope",
                        ERROR_DLG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return srcTrack;
        }

        //--------------------------------------------------------------------------------------------------
        //! \brief      Obtain the list of destination tracks for replicating volume envelope
        //! \param[in]  sourceTrack - SourceTrack obtained with FindSourceTrack()
        //! \return     List with all other tracks from the same group as sourceTrack.
        public List<Track> GetDestTracks(Track sourceTrack)
        {
            List<Track> rslt = new List<Track>();
            TrackGroup grp = GetTrackgroup(sourceTrack);
            if (grp != null)
            {
                foreach (Track t in grp)
                {
                    if (t != sourceTrack)
                    {
                        rslt.Add(t);
                    }
                }
            }
            return rslt;
        }


        //--------------------------------------------------------------------------------------------------
        //! \brief      Get the trackgroup to which a track belongs
        //! \param[in]  track 
        //! \return     Trackgroup to which the track belongs or null if the track is not a member of a group
        private TrackGroup GetTrackgroup(Track trk)
        {
            TrackGroup rslt = null;
            foreach (TrackGroup grp in m_vegas.Project.TrackGroups)
            {
                if (grp.Contains(trk))
                {
                    rslt = grp;
                    break;
                }
            }
            return rslt;
        }
        private Vegas m_vegas;
    } 
} // namespace 

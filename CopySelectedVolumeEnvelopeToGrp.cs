/* 
 *  File: 
 *  Description:  Vegas-pro script, which copies the volume envelope from selected events to all other tracks within the same track group.
 *                Conditions:   Only events from a single track may be selected.    
 *                              The track containing the events must be a member of a track group.
 * Author:        Marcel Gielen
 * Date:          04-02-2023
 * Tested with:   Vegas Pro V19.0.648 
*/

using ScriptPortal.Vegas;
using System.Collections.Generic;
using System.Windows.Forms;

namespace volumeEnvelope
{
    public class EntryPoint
    {
        public void FromVegas(Vegas vegas)
        {
            TrackGroupVolEnvelopeCopier volEnvCopier = new TrackGroupVolEnvelopeCopier(vegas);

            Track srcTrack = volEnvCopier.FindSourceTrack();
            if (srcTrack != null)
            {
                volEnvCopier.CopyVolumeEnvelopeSelection(srcTrack, volEnvCopier.GetDestTracks(srcTrack));
            }
        }
    } // class entrypoint
}





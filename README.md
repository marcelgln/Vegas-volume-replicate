# Vegas-Pro volume envelope replicator
## Why this script was created
There are many types of workflow which can be used when using an NLE for video.  I often render to 5.1 surround, using 5.1 recorded camera sources. This results in 2 stereo audio tracks (Front R/R, Surround L/R) and 2 mono tracks (Center/LFE) . When you want to combine this with background music, and want to reduce  the volume level of the CAM audio at certain positions, then it is necessary to add volume envelope points to each of the tracks. Adjusting afterwards means  adjusting individual points on each of the tracks. This is a time consuming operation. 
Initially I expected to use an 'audio-bus' for this: assign the 4 tracks to an audio -bus, use an audio bus track with a volume envelope and you are done. Unfortunately Audio busses in Vegas do not support multi channel audio and you loose the panning information from the individual tracks. 
I am not under the impression that the Vegas team is going to improve this (I have already seen complaints about this limitation  going back to 2011), so I decided to give the C# scripting a try, with this a (at least for me) usable result. 
## How it works
The whole intention was to make it easier to keep the volume envelopes of multichannel audio synchronized. For this script to work, the related audio tracks must be grouped in a track group. Then select some event from which the envelope points must be replicated to the other tracks within the same group andthen run the script. 

## Conditions
A few conditions must be met, when running the script to determine the correct source. 
 - Only events on a single track may be selected
 - The track must be a member of a track group  
 
A dialog is shown in case of errors.   

## Installation  
Copy the file GroupVolumeEnvelopeReplicate.dll from the bin folder to one of the Vegas Script folders (e.g. C:\Users\All Users\VEGAS Pro\<version>\Script Menu\) Do not install it in the 'Script Menu' folder of the Vegas installation directory, since that might be deleted during updates. 

## Disclaimer
I usually do not use c#. The script has currenlty only been tested briefly, so use it at your own risk. It has only been tested with Vegas-Pro 19.



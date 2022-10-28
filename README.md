# BottangoTeddyDriver

A C# WinForms app for reading and parsing Teddy Ruxpin tapes, as well as generating animation signals from multiple sources including Bottango

# Features:

- Read and parse existing WoW Teddy Ruxpin tape recordings (WAV format tested)
- Act as [Bottango](https://bottango.com) network driver
- Monitor default microphone input or speaker (WASAPI Loopback) output and translate them into facial movements

### Turn abovementioned sources into live animation data with an optional loopback (routed to teddy's speaker)

# Sources:

- The library `NAudio` is from NuGet, written by Mark Heath. [Source](https://github.com/naudio/NAudio)
- The project `AudioPPM` is originally from Garrus007's solution PPMControl, although it has been modified. [Source](https://github.com/Garrus007/PPMControl)

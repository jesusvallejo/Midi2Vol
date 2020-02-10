# Midi2Vol

<img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoSlider.png" width="60">  <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoBento.png" width="60"> <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoWavez.png" width="60">



Windows Volume Control for Nano. Slider


This is mainly developed for Nano. Slider, but it can be fairly easily used with any Midi based potentiometer. 
It is written only for Windows and wont work on any other plataform as it has to be written in OS compatible language (C# in this case with .net framework).

I'll provice a binary installer but i recommend you to compile it by yourself. 
It is provided as is , and it comes with no guarantee. 
Nevertheless any change is welcomed and will try to make it as efficient as possible.

This project uses the following libraries:
           - AudioSwitcher.AudioApi.CoreAudio : https://github.com/xenolightning/AudioSwitcher
           - NAudio: https://github.com/naudio/NAudio

TODO
- [x] Msi installer
- [x] Edition Icon
- [ ] Set proper public/private flags to code
- [x] Separate classes into multiple files to improve readability
- [x] Add auto launch on boot
- [x] Add menu to Apply/Remove auto launch on boot
- [ ] Test stability

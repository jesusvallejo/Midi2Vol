# Midi2Vol
English | [Español](./README-es.md)



<img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoSlider.png" width="180">  <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoBento.png" width="180"> <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoWavez.png" width="180">



Windows® Volume Control for Nano. Slider


This is mainly developed for Nano. Slider, but it can be fairly easily used with any Midi based potentiometer. 
It is written only for Windows® and wont work on any other plataform as it has to be written in OS compatible language (C# in this case with .net framework).

The app lives in the tray in order to be less intrusive, for the moment the only options are to exit the app and to Add/remove the StartUp run.
During the app install,it'll be set to run on Start Up by default, if you don't want to run it each boot, just remove it with the Add/remove button.

There are three versions,all three are the same but the icon, which can match your Nano. Slider's appearance, in the future there will be an option to configure it instead of multiple installers.

I'll provice a binary installer but i recommend you to compile it by yourself. 
It is provided as is, and it comes with no guarantee.(see [Licence](https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/LICENSE))

Nevertheless any change, update or upgrade is welcomed.

This project uses the following libraries:

- ~~AudioSwitcher.AudioApi.CoreAudio : https://github.com/xenolightning/AudioSwitcher~~ 
Deprecated now it only uses NAudio, AudioSwitcher was slow at init
- NAudio: https://github.com/naudio/NAudio

TODO
- [x] Update code to better integrate official name: Midi2Vol
- [x] Msi installer
- [x] Edition Icon
- [x] Separate classes into multiple files to improve readability
- [x] Add auto launch on boot
- [x] Add menu to Apply/Remove auto launch on boot
- [x] Add Hot-Plug support
- [x] Solved Bug: wont work after sleep mode
- [ ] One Msi installer, multiple icons that can be selected to fit any Nano. Slider appearance
- [ ] Ask on install , whether to run on Startup or not
- [x] Set proper public/private flags to code
- [x] Test stability

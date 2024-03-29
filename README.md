# Midi2Vol
English | [Español](./README-es.md)





<img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoSlider.png" width="180">  <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoBento.png" width="180"> <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoWavez.png" width="180">



Windows® Volume Control for Nano. Slider -- [Linux](https://github.com/jesusvallejo/Midi2Vol-Linux)

This is mainly developed for [Nano. Slider](https://www.keebwerk.com/nano-slider/), but it coul be fairly easily ported to any Midi based potentiometer. 
It is written only for Windows®([Linux Version](https://github.com/jesusvallejo/Midi2Vol-Linux)).

The app lives in the tray in order to be less intrusive.

This version has per app volume control(Windows mixer, could be Spotify , Google Chrome) as well as current device volume control(Earphones,Speakers), it can be configured via the config menu button or editing config.json,to add/edit more apps.(config.json is under ```\user\AppData\midi2vol```, it is recomended to use the menu configurator to avoid parsing errors).

In order to make it work you will need to add some code to your keymap,or pehaps use the one i provide here: https://github.com/jesusvallejo/nanokeymaps/

Remember to use different hex values. Default volume value is: ```0x3E```.

I'll provide [binary files](https://github.com/jesusvallejo/Midi2Vol/releases) but i recommend you to compile it by yourself.Binaries are unsigned as the certificate costs money per year.

Check [KeebWerk Support](https://www.keebwerk.com/support) for an in depth installation and use guide.

It is provided as is, and it comes with no guarantee.(see [Licence](https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/LICENSE))

Nevertheless any change, update or upgrade is welcomed.

This project uses the following libraries:

Midi handling:
- NAudio: https://github.com/naudio/NAudio

Volume handling:
- CSCore: https://github.com/filoe/cscore

TODO
- [ ] Check wether the AppRaw input in config menu is an hex.
- [ ] Check .json parsing errors


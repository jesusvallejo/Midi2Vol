# Midi2Vol
English | [Español](./README-es.md)





<img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoSlider.png" width="180">  <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoBento.png" width="180"> <img src="https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/ReadResources/NanoWavez.png" width="180">



Windows® Volume Control for Nano. Slider -- [Linux](https://github.com/jesusvallejo/Midi2Vol-Linux)

This is mainly developed for Nano. Slider, but it can be fairly easily used with any Midi based potentiometer. 
It is written only for Windows® and wont work on any other plataform as it has to be written in OS compatible language (C# in this case with .net framework).

The app lives in the tray in order to be less intrusive, for the moment the only options are to exit the app and to Add/remove the StartUp run.

This version has per app control when using pulse, it can be configured via the config menu button or editing config.json,to add/edit more apps.In order to make ti work  we have to change some things on the qmk keymap,instancitate an app variable as 0x3E,``` uint8_t app = 0x3E; ``` , on slider function we have to change midi_send_cc to ```midi_send_cc(&midi_device, 2, app, 0x7F - (analogReadPin(SLIDER_PIN) >> 3));``` and last use the macro utility to change ``` app ``` value to what ever is configured in the config.json

ex:
```
uint8_t app = 0x3E;

// Defines the keycodes used by our macros in process_record_user
enum custom_keycodes {
    QMKBEST = SAFE_RANGE,
    SPOTIFY,DISCORD
};
bool process_record_user(uint16_t keycode, keyrecord_t *record) {
    switch (keycode) {
        case SPOTIFY:
            if (record->event.pressed) {
                // when keycode SPOTIFY is pressed
                app= 0x3F;
            } else {
                app= 0x3E;
                // when keycode SPOTIFY is released
            }
            break;
        case DISCORD:
            if (record->event.pressed) {
                // when keycode SPOTIFY is pressed
                app= 0x40;
            } else {
                app= 0x3E;
                // when keycode SPOTIFY is released
            }
            break;
    }
    return true;
}
void slider(void) {
    if (divisor++) { // only run the slider function 1/256 times it's called
        return;
    }

    midi_send_cc(&midi_device, 2, app, 0x7F - (analogReadPin(SLIDER_PIN) >> 3));
}
```

I'll provice a binary files but i recommend you to compile it by yourself. 
It is provided as is, and it comes with no guarantee.(see [Licence](https://raw.githubusercontent.com/jesusvallejo/Midi2Vol/master/LICENSE))

Nevertheless any change, update or upgrade is welcomed.

This project uses the following libraries:

Midi handling:
- NAudio: https://github.com/naudio/NAudio

Volume handling:
- CSCore: https://github.com/filoe/cscore

TODO
- [x] Add auto launch on boot
- [x] Add menu to Apply/Remove auto launch on boot
- [x] Add Hot-Plug support
- [x] Test stability
- [X] Allow control when changing audio output Devices
- [x] Allow per App volume control


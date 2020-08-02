# Big 2 / capsa
  - Develop with:
    - Unity version 2019.2.16f1 on Ubuntu 18.04
    - Inkscape
    - Unity Plugins:
      -	SimpleJSON (https://github.com/Bunny83/SimpleJSON)

  - Game reference: https://play.google.com/store/apps/details?id=com.emagssob.big2&hl=en_US

  - Game rules: https://www.pagat.com/climbing/bigtwo.html
    
  - assets directory:
    - scenes directory: Assets/Scenes/
    - scripts directory: Assets/Scripts/
    - images directory: Assets/Resources/images/
    - plugins directory: Assets/Scripts/Plugins/
    - json directory: Assets/Resources/JSON/

```
Assets/Scenes/MainGame.unity
Assets/Scenes/AvatarSelection.unity
Assets/Scripts/AvatarSelection
Assets/Scripts/AvatarSelection/AvatarBox.cs
Assets/Scripts/AvatarSelection/Main_Ava.cs
Assets/Scripts/MessageSender.cs
Assets/Scripts/MainGame/Main.cs
Assets/Scripts/MainGame/Debugger.cs
Assets/Scripts/MainGame/Card.cs
Assets/Scripts/MainGame/Player.cs
Assets/Scripts/MainGame/SetPanel.cs
Assets/Scripts/MainGame/Card_Game_Object.cs
Assets/Scripts/MainGame/ProfilePanel.cs
Assets/Scripts/Plugins/SimpleJSON.cs
Assets/Scripts/Plugins/LICENSE
Assets/Resources/JSON/cards.json
```
    
  - what works?
    - Avatar selection scene. Player can choose avatar and it will affect profile photo.
    - Profile photo can change depending on the game condition during gameplay. 
      For example, when winning the character is smiling, 
      while when losing the character looks sad / angry.
    - For now, we only play with single, pair, and triple set.
    - Game start from player -> com1 -> com2 -> com3 -> player.
    - No main menu, only avatar selection and gameplay.
    - No loading screen (of course... what to expect...)

some screenshoot: </br>
![Alt text](Screenshoot/avatarselection.png?raw=true "avatar selection screen")
![Alt text](Screenshoot/gameplay.png?raw=true "game play screen")

debugging screenshoot: </br>
![Alt text](Screenshoot/gameplay2.png?raw=true "debugging game play screen")

``` 
Kevin Haryo Kuncoro
kevinhyokun91@gmail.com
2020 
```

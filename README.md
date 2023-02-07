# Quest System v2.1
### Unity version: 2022

- Added (07.02.2023)
  - Achievements

![img](https://github.com/paveldrobny/Unity_QuestSystem/blob/main/QuestSystem4.png?raw=true)
<br/>
![img](https://github.com/paveldrobny/Unity_QuestSystem/blob/main/QuestSystem5.png?raw=true)
<br/>

- Added (26.12.2022)
  - Level system & Getting experience for completing quests
  - Available quests count
  - GameSystem for spawn Quests UI and Quest manager
- Fixed
  - Various minor errors

### Replaced UI methods, for example: 

##### Before:
```C#
    ui_ConfirmQuest.setActive(true);
```  
##### After:
```C#
    QuestConfirmUI.Instance.Show();
```

Demo level path: "Assets/StarterAssets/ThirdPersonController/Scenes/Playground"

![img](https://github.com/paveldrobny/Unity_QuestSystem/blob/main/QuestSystem.png?raw=true)
<br/>

![img](https://github.com/paveldrobny/Unity_QuestSystem/blob/main/QuestSystem1.png?raw=true)
<br/>

![img](https://github.com/paveldrobny/Unity_QuestSystem/blob/main/QuestSystem2.png?raw=true)
<br/>

![img](https://github.com/paveldrobny/Unity_QuestSystem/blob/main/QuestSystem3.png?raw=true)

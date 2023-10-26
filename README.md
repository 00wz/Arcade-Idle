Arcade Idle Prototype
===
>  _В ReadMe укажите свои Ф. И. О._: **Тесленко Павел Григорьвич**
---

Базовый класс для интерактивных объектов на сцене: [InteractableObject](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/InteractableObjects/InteractableObject.cs)
объявляет [стейтмашину](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/StateMachine/StateMachine.cs), которая инициализируется в наследниках и передает в нее
[персонажа](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/ICharacter.cs) при взаимодействии. Так же имеет [SceneMessage](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/UI/SceneMessage.cs) для минимальной демонстрации работы. **Наследники:**

[Mine](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/InteractableObjects/Mine/Mine.cs): источник ресурсов. при истечении определенного лимита требуется время для 
перезарядки (рудник, лес, шахта). в инспекторе настраивается вид ресурса, лимит, скорость добычи, и время на перезарядку.

[Container](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/InteractableObjects/Container/Container.cs): объект, содержимое которого можно открыть за определенное колво ресурсов.
В инспкеторе настраивается список ресурсов необходимых для открытия контейнера.

---

Добавление и редактирование видов ресурсов в [/Assets/Configs
/CurrencyConfig.asset](https://github.com/00wz/Arcade-Idle/tree/main/Assets/Configs), при этом автоматически обновляется enum для простого использования в коде и инспекторе.

---

Контейнером для глобальных объктов на сцене является [GameRootInstance](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/GameRootInstance.cs), который доступен через статическое поле,
и через инспетор ссылается на объекты:

[NavMashUpdater](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/NavMashScripts/NavMashUpdater.cs): обновляет NavMash при появлении на сцене объектов имеющих компонент 
[NavMeshSource](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/NavMashScripts/NavMeshSource.cs). 
> NavMash используется для передвижения персонажа и нпс(Not implemented yet)

[MainInventory](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/MainInventory.cs) для хранения ресурсов игрока

---
Класс [Saver](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/Saver/Saver.cs) регистрирует скрипты на сцене, которые реализуют интерфейс
[ISaveble](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/Saver/ISaveble.cs) и реализует сохранение и загрузку при вызове в нем соответсвующих публичных методов.

[AutoSaveCaller](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/Saver/AutoSaveCaller.cs) инициализирует
[Saver](https://github.com/00wz/Arcade-Idle/blob/main/Assets/Scripts/Saver/Saver.cs) и вызывает загрузку и сохранение при старте и выходе из игры.

---
пример игровой сцены: [SampleScene](https://github.com/00wz/Arcade-Idle/tree/main/Assets/Scenes)

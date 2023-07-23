**KEYBOARD TRAINER WPF APPLICATION**
<hr>

**What is a keyboar trainer:**
Keyboard Trainer is a software tool designed to improve your typing skills.
The main functionality of the keyboard simulator is to test users for fast and accurate text entry.

<hr>

**Work with database:**
To store the data, I used a SQLite file database and to connect to the database, we used the Entity Framework, which simplifies database work through an object-oriented approach. Entity Framework automatically generates SQL queries and provides mapping between objects and tables, as well as provides ORM functionality and integration with LINQ.

To implement the work with the database, I used the Dependency Injection pattern, which allows you to create all program dependencies in one place, passing them to components through the constructor. It also promotes the use of the Singleton pattern, which means that objects of certain classes exist only in a single instance. In general, Dependency Injection makes program modules flexible because dependencies can be easily changed or replaced without changing the code of these modules.

<hr>

**Architerture:**
When developing the keyboard trainer, I decided to split the solution into 4 layers using a service architecture approach:

+ **KeyboardTrainerDAL** - a database access layer with the database itself and the DataContext class for tracking data through the Entity Framework.

+ **KeyboardTrainerModel** - the model layer, it contains models of entities stored in the database. It also contains interfaces that are designed to work with each of the models.

+ **KeyboardTrainerServices** - contains services that implement interfaces with the KeyboardTrainerModel. This provides code flexibility, allowing you to replace the ORM system as needed. (This is done to reduce dependencies and increase the flexibility of the application code. For example, you may need to use another ORM system (for example, **NHibernate or Dapper**), so you can implement services to work with it by implementing interfaces with the KeyboardTrainerModel, replace classes at the program startup point (this is provided by the Dependency Injection pattern) and continue the necessary work without wasting time rewriting all the connections to the database in the program)

+ **KeyboardTrainerWPF** - a layer of visual representation of the program, where all other logic is implemented using the MVVM pattern

So, the course work solution consists of 4 projects (layers), 3 of which are class libraries and one WPF project.

<hr>

**Class Diagram:**
![ClassDiagram](https://github.com/IIIA-KO/KeyboardTrainer-WPF-MVVM/assets/108343976/4f8903d9-90c4-4327-9450-5ffe69b11922)

<hr>

**Interface:**
+ **Home Page**
    ![HomePage](https://github.com/IIIA-KO/KeyboardTrainer-WPF-MVVM/assets/108343976/2b2763aa-f6ac-42f1-92fc-eb9183278c88)

<br>

+ **Records Table Page**
    ![RecordsTable](https://github.com/IIIA-KO/KeyboardTrainer-WPF-MVVM/assets/108343976/63e2797e-5e0d-461b-a688-17192e7926bc)

<br>

+ **Settings Page**
    ![SettingsPage](https://github.com/IIIA-KO/KeyboardTrainer-WPF-MVVM/assets/108343976/07e69eb2-28e7-42c3-a82e-adf8ef958a1b)

<br>

+ **LogIn/SignUp Page**
    ![Account](https://github.com/IIIA-KO/KeyboardTrainer-WPF-MVVM/assets/108343976/1de3a744-b4d4-494c-97e2-395488b02425)

<hr>

**Typing process:**
![](https://github.com/IIIA-KO/KeyboardTrainer-WPF-MVVM/assets/108343976/95acb6b3-a43d-4bc0-9032-c4a2bba11223)

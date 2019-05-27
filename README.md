# HexagonRobot

## Notes

* The Application is built using .NET CORE 2.2 on Visual Studio 2019.
* The Application took 3 days to build with an average of 8 hours a day being spent on the project.
* There was a bit of Unit Testing added using the XUnit Framework.

## Running the Application

* Open the solution in Visual Studio.
* Run the Application.

    ![Run Application](https://github.com/jwong1512/HexagonRobot/blob/master/Images/run-application.PNG)

* You should see the following Welcome Screen.

    ![HexagonRobot Welcome Screen](https://github.com/jwong1512/HexagonRobot/blob/master/Images/welcome-screen.PNG)

* Type the x-coordinate, y-coordinate and cardinal direction of where you want the robot to start. The x-coordinate and y-coordinate must be either **[0,1,2,3,4]**. The cardinal direction must be either **[North, South, East, West]**. The command must be written in the format: 

        Place 0, 0, North

    ![Initial Place Command](https://github.com/jwong1512/HexagonRobot/blob/master/Images/initial-place-command.PNG)

* The next screen will prompt you to select an option. Each option has a corresponding number. Typing the number and pressing **ENTER** will select that option. 

    ![Option Screen](https://github.com/jwong1512/HexagonRobot/blob/master/Images/option-screen.PNG)

* The options are as follows.

| Number  | Option          | Description
| ------- | --------------- | --------------------------------------------------------------------------------------------
|    1    | MOVE            | Moves the Robot one step in the direction it is currently facing.
|    2    | LEFT            | Rotates the Robot anti-clockwise 90 degrees from the direction it is facing.
|    3    | RIGHT           | Rotates the Robot clockwise 90 degrees from the direction it is facing.
|    4    | REPORT          | Displays the current x-coordinate, y-coordinate and direction of the Robot.
|    5    | TYPE COMMAND    | This allows you the ability to TYPE in the commands manually instead of pressing a button.
|    6    | INPUT FILE      | Reads a set of instructions from a Text File and executes them one by one.
|    7    | QUIT            | Exits the Application.

## The TYPE COMMAND Option

The **TYPE COMMAND** option allows you the ability to type in the commands *Manually* instead of just selecting a number which then executes the command for you.

The following are a set of instructions on how to use the **TYPE COMMAND**.

* On the Options Screen type in the number **5** and press **ENTER** to select option to type in the commands  *Manually*.

    ![Type Command](https://github.com/jwong1512/HexagonRobot/blob/master/Images/type-command-selection.PNG)

* You should see the following screen which lists all the commands you can type. Type in a command and press **ENTER** to execute the command.

    ![Type Command Options](https://github.com/jwong1512/HexagonRobot/blob/master/Images/type-command-options.PNG)

## Text File Instructions

Here are the instructions to execute commands written in a Text File.

* There is a folder within this project called **Input File** and inside this folder is a text file called **Instructions.txt**. This is the text file where the commands are located.

    ![InputFile Folder](https://github.com/jwong1512/HexagonRobot/blob/master/Images/inputfile-folder.PNG)

* Type the commands you want executed inside this file.

    ![Instructions.txt](https://github.com/jwong1512/HexagonRobot/blob/master/Images/instruction-file.PNG)

* Navigate to the *Options Screen*, and type in **6** and then press **ENTER**. This will then execute the commands listed in the **Instructions.txt** file.

    ![Instructions.txt](https://github.com/jwong1512/HexagonRobot/blob/master/Images/select-instruction-commands.PNG)

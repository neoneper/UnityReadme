# UnityReadme:
UnityReadme is a simple but functional system, allows you to add Readme in asset format to the project and scene objects.

This is intended to improve communication with the team working on the project and also to allow other team members who are not actively linked to the script but rather activities directly linked in the editor, such as Level-Designers, to help document the project and the components that it uses.

In addition to helping to create visual points of interest through the hierarchy of unity.
***

Project Readme:

![preview](https://i.gyazo.com/57d69114f4d1d6b6c89ccf8b3ecbc36b.png)

Scene Object Readme:

![preview2](https://i.gyazo.com/a09e6e51fb6ec005be727720107c9e7a.png)


- Use this to organize important information about your project, such as: 
  - How to use tutorial;
  
  - Link utils;
  
  - Important information for the team; 
  
  - Hierarchy organizations with icons and informations about the GameObject
  
  - etc..

- Tested at Unity 2019.4+

# Features!
***

- Allow HTML Tags at richText format;
- Project Readme: Auto Open at project launch; 
- Project Readme: Open with tutorial Button at unity toolbar.
- Scene Objects Readme;
- Hierarchy icon to scene objects readme;
- Field to URL adress.

# Installation
***

Just add the Readme folder to your project. Everything will be working from there.
The readme folder contains all the scripts for correct system functionality.
This folder also contains a pre-configured Readme file with some information that you can view for inspiration.
You can add as many reading files as you like, but only one should be used as the project's readme. If there is more than one, the system should select one at random. So make sure you have only one readme file set to (Root).

# How to create readme files:
***

1 - RightClick at [Project - View];

2 - Selec: Create->Readme, at context menu.

3 - A Readme file will be created at some selected folder that you has selected, or at Assets.

# How to edit readme files:
***

By default, files read will be created in edit mode. This means that the fields for filling in the user will be available.
To view the formatted file, you need to deselect the option (Show in edit mode).
You can always return to edit mode to make new updates to the file.

![readme_edit_mode](https://i.gyazo.com/930d4f4533fe7419a5bb9bac34af3b49.png)

- **Icon**: A selected icon image, showed at left side of the Title of the readme;

- **Title**: The Title of the readme, showed at right side of the icon.

- **Sections**: contents of your readme file. This is a list that contains predefined fields that you can choose to fill in as needed. Each field has a different formatting type in the preview mode. All completed fields will be shown vertically and unfilled fields will simply be ignoreds.

![readme_sections](https://i.gyazo.com/b092cc3b8f05bb342bc7415514f6581a.png)

- **Heading:** Big Text with Bold Format.
- **Text**: Simple Text, without format.
- **Link Text**: Title of the link with URL Format.
- **Url**: Link fot some URL adress.

![readme_editmode_toolbar](https://i.gyazo.com/6f8248da0ccfc4cb8cd17d716beef7cb.png)

- **Show in Edit Mode:** Use it to change for formated or edit mode.

- **Set as Root:** Root reademe will be loaded and showed automaticaly when the project has been launched. Keep only one Root Readme. If exist more, the system will be use a random root readme.

- **Update Sections Label**: Use this to update the name of the elements of the list. When clicked, all the fields will be filled with initial words of the 
phrase.

# How to add Reade to Scene Objects:
***
1 - Create a readme in advance, following the previous tips.

2 - Select some GameObject at Scene and add the componnent (Readme);

3 - At the component, select a readme file to show here.

4 - Uncheck the (Change Readme File) box.

5 - Check (Change Readme File), if you want change the readme reference of this component.

6 - Check/Unchek (Show icon in Hierarchy) option, to show or hide the hierarchy object readme icon.


![behaviourgiff](https://i.gyazo.com/f96975c2cd206dbbe728a989571f5ca8.gif)

# Show as Tutorial Action:
***

1 - Click at [Tutorial] Button at Unity Top Toolbar.

2 - The Readme setted as Root, will be showed at inspector.

# How to show automaticaly the project readme when project is launched!

Simply mark some readme as (root) in editor mode. This will start it automatically when the project is opened.
Remember to keep only one readme with this option active. If there is more than one readme with this option, the system should choose one at random.

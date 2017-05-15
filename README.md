# G54SPM_GroupD_SATO

The Project:

SATO is a game that is aimed at treating amblyopia, while remaining far more enjoyable than other treatments, 
such as patching.  It will play much in a similar way to Galaga, and many other retro-shooters.  It will
be displayed on a VR headset; however not all of the same information will be displayed across both screens.
To succeed at SATO, information that is mutually exclusive to each screen has to be combined, thus forcing
both eyes to focus at all times and cooperate.

For more information, read the Design Documentation in the link below.

Design Documentation Links:

User Stories: https://docs.google.com/document/d/10_wt7cGKeoRx8ZQ8n2sLYxnzZTv7Q3PusyDOetIm0Tg/edit?usp=sharing
Mid-Fidelity Document Link: https://docs.google.com/document/d/19Or5JE1Jney1VjZ5MJZbMemWfzKyWkqDQodDYxCPI-0/edit?usp=sharing

Our Trello Board:

https://trello.com/spmgroupd

Conventions Document Link:

https://docs.google.com/document/d/1GmDLD1p4A-1mWhBsHAR8BpqlI6U3gwNuH5TqvuoGDm4/edit?usp=sharing

NOTE: If you clone the repository, be wary of how you run unit tests.  We are currently working on fixing a bug where the
High Score screen will appear above the in-game screen if you run the unit tests in the Main_Scene scene.  Merely switch
back to the Main_Menu scene and DO NOT save changes.  The tests are intended to be ran in the Main_Scene scene, not the
MainMenu scene due to the tests using in-scene elements.
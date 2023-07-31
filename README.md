# CapstoneProject

Creators: Ali Ibrahim, Mickey Girmai, David Oganov

Lingomon:

SCOPE
The overall scope of our project is to provide users with a playable environment suited to provide them with a language learning experience. 
Detailed listing below:

STORY
    The story will follow the user, a new kid in a town, ready to receive their own Lingomon from Professor Larry. 
    The user will then follow Professor Larry’s instructions to assist the professor in developing a brand new Glossary for the world of Lingomon. 
    The tasks will involve defeating various NPC trainers, answering their questions in order to proceed. 
    A total of 3 trainer battles, and 1 final boss battle.  

REGION
    Each region will focus on a separate aspect of language learning 
    Grammar, spelling, punctuation.

QUESTIONS
    Vary depending on the region topic
    Stored with corresponding answers in PostgreSQL database on Azure Flexible Server.
    Have designated difficulty level
    Classified as strength within the PostgreSQL database.

DAMAGE
    Amount varies depending on the difficulty, strength, of the question being answered.

TRAINER BATTLES
    Several questions, specific to the region, being asked of the user. If the user responds correctly, their Lingomon will deliver damage to its opponent; otherwise, the opposing Lingomon will deliver damage.

 FINAL BOSS BATTLE
    Questions with a varying number from each playable region.
    Leads to the conclusion of the story of Lingomon. 

There are several features that we would like to implement but will not likely have the ability to implement within our provided time constraints. 

    More regions and NPC’s within each region, 
        Provides room for growth in variety of questions
        Provides extendability of in-game story
    Increased number of available languages
        Provide a wide range of choice in language learning
    Ability to find and pick up items in the wild, for trainers
        Items may be used on Lingomon, in trainer battles as question hints, and decor for user Lingo Huts.
    Milestone Achievements
        Provide a ‘User Score’ totaling over all saved Lingomon worlds on the users’ account. 
        Milestones include achievement lists for each language available to the user. 
    Detailed implementation of region environments
        Weather alterations
        Detailed housing decorations relating to the culture of the languages completed on users' Lingomon journey.
        Availability for student to create ‘Lingo Hut’

GOAL
The main objective of this Unity game project is to develop an engaging, enjoyable, and interactive learning tool aimed at offering young language beginners a unique and simple method of learning.   

VALUE
    Multilingualism is the ability to speak, understand, or use multiple languages effectively in collaborative and inclusive environments. It promotes communication and collaboration among individuals regardless of their native languages. Encouraging multilingualism in a language learning game can diversify players' linguistic backgrounds and enhance their language skills.
    Incorporable into modern curriculums, increasing multilingualism and diversity.  
    Encouraging and inviting avenue of learning that is intuitive for young minds
    Emulates the important aspects of successful language learning; diverse contexts amongst questions, enforced repetition, and testing of mastery.

INTENDED AUDIENCE
    The targeted audience are students [ages 8-14] looking to learn multiple languages or refine their knowledge on a currently spoken language.
    Fans of Pokemon or other similar media
    Familiarity and moderate competency with technology, specifically in regards to video games
    Students looking to increase their knowledge of language and culture.

USER NEEDS
    Used as an integration into school curriculums.
    Gameplay motivates users to practice and explore the given language learning topics
    The game is intuitive and includes the needed instructional material to effectively play the game.
    Enjoyable learning method meant to maintain engagement of users throughout the learning process.

ASSUMPTIONS AND DEPENDENCIES
    User is informed and experienced on how to use their device and needed peripherals, prior to starting the game
    User has a beginner level understanding of the language they are trying to learn

DEFINITIONS & ACRONYMS
    NPC: Non-Playable Character, NPCs are characters found in the game that the user interacts with, but does not play as.
    Lingomon: the name of the creatures fought and captured in the game. The user fights Lingomon in the wild or by battling NPCs and answers language learning questions in these fights.
    Random Encounters: the user is able to fight Lingomon in the wild by running through bushes found in the game’s map. After a random amount of time in these bushes, the player will enter a “random encounter” against a Lingomon and begin combat.
    Lingo trainers: Lingo trainers are NPC’s found across the game that allow the player to engage in language learning combat similar to random encounters. 
    Lingo specialist: In order for the user to complete their learning in a region of the map, they must successfully beat the Lingo specialist of that region. The Lingo specialist is a NPC that has a wide range of Lingomon that cover all the learning topics of the region. It is intended to be a difficult fight that verifies if the student has learned the topics of the region.
    Testing hall: Houses a region’s lingo specialist. Every region has one testing hall for their one lingo specialist. Students enter the testing hall in order to start combat with a lingo specialist.
    Professor Larry: the player’s guide through the game. Larry assigns tasks to the player that are to be completed in order of assignment to progress through the game correctly
    Lingo Hut: A set base the user may establish to decorate with items found on their journey through Lingomon. These huts store a collection of all completed milestones. 


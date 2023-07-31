# CapstoneProject

**Creators**: Ali Ibrahim, Mickey Girmai, David Oganov

## Lingomon

### Scope
The overall scope of our project is to provide users with a playable environment suited to provide them with a language learning experience. The project includes the following elements:

### Story
The user plays as a new kid in a town, ready to receive their own Lingomon from Professor Larry. The user follows Professor Larry’s instructions to assist in developing a brand new Glossary for the world of Lingomon. Tasks involve defeating various NPC trainers and answering their questions to proceed. There are a total of 3 trainer battles and 1 final boss battle.

### Regions
Each region focuses on a separate aspect of language learning: Grammar, spelling, and punctuation.

### Questions
The questions vary depending on the region topic. They are stored with corresponding answers in a PostgreSQL database on Azure Flexible Server. Each question has a designated difficulty level and is classified as strength within the PostgreSQL database.

### Damage
The amount of damage varies depending on the difficulty and strength of the question being answered.

### Trainer Battles
During trainer battles, several questions specific to the region are asked of the user. If the user responds correctly, their Lingomon will deliver damage to its opponent; otherwise, the opposing Lingomon will deliver damage.

### Final Boss Battle
The final boss battle consists of questions from each playable region and leads to the conclusion of the story of Lingomon.

### Planned Features (Not within Time Constraints)
While there are several features we would like to implement, they are not likely to be possible within our provided time constraints. These features include:

- More regions and NPC’s within each region, providing a variety of questions and an extendable in-game story.
- Increased number of available languages, offering a wide range of choices in language learning.
- Ability to find and pick up items in the wild for trainers, which can be used as question hints during battles and decor for user Lingo Huts.
- Milestone Achievements, providing a 'User Score' based on all saved Lingomon worlds on the users' account, and achievement lists for each language available to the user.
- Detailed implementation of region environments, including weather alterations and culturally related housing decorations based on the languages completed on the user's Lingomon journey. This will also include the option for students to create their own 'Lingo Hut.'

### Goal
The main objective of this Unity game project is to develop an engaging, enjoyable, and interactive learning tool aimed at offering young language beginners a unique and simple method of learning.

### Value
Multilingualism is the ability to speak, understand, or use multiple languages effectively in collaborative and inclusive environments. Encouraging multilingualism in a language learning game can diversify players' linguistic backgrounds and enhance their language skills. This project aims to be:

- Incorporable into modern curriculums, increasing multilingualism and diversity.
- An encouraging and inviting avenue of learning that is intuitive for young minds.
- Emulating the important aspects of successful language learning, including diverse contexts among questions, enforced repetition, and testing of mastery.

### Intended Audience
The targeted audience for this project includes:

- Students [ages 8-14] looking to learn multiple languages or refine their knowledge of a currently spoken language.
- Fans of Pokemon or other similar media.
- Individuals with familiarity and moderate competency with technology, specifically in regards to video games.
- Students looking to increase their knowledge of language and culture.

### User Needs
Lingomon is designed to address the following user needs:

- Integration into school curriculums to facilitate language learning.
- Motivating users to practice and explore language learning topics through engaging gameplay.
- An intuitive game that includes the necessary instructional material to effectively play the game.
- An enjoyable learning method that maintains user engagement throughout the learning process.

### Assumptions and Dependencies
To use this game, users are assumed to:

- Be informed and experienced in using their devices and needed peripherals prior to starting the game.
- Have a beginner level understanding of the language they are trying to learn.

### Definitions & Acronyms
- **NPC**: Non-Playable Character, NPCs are characters found in the game that the user interacts with but does not play as.
- **Lingomon**: The name of the creatures fought and captured in the game. The user fights Lingomon in the wild or by battling NPCs and answers language learning questions during these fights.
- **Random Encounters**: The user can fight Lingomon in the wild by running through bushes found in the game's map. After a random amount of time in these bushes, the player will enter a "random encounter" against a Lingomon and begin combat.
- **Lingo Trainers**: Lingo trainers are NPCs found across the game that allow the player to engage in language learning combat similar to random encounters.
- **Lingo Specialist**: To complete their learning in a region of the map, the user must successfully beat the Lingo specialist of that region. The Lingo specialist is an NPC that has a wide range of Lingomon that cover all the learning topics of the region. It is intended to be a difficult fight that verifies if the student has learned the topics of the region.
- **Testing Hall**: It houses a region’s Lingo specialist. Every region has one testing hall for its one Lingo specialist. Students enter the testing hall to start combat with a Lingo specialist.
- **Professor Larry**: The player’s guide through the game. Larry assigns tasks to the player that are to be completed in order of assignment to progress through the game correctly.
- **Lingo Hut**: A set base the user may establish to decorate with items found on their journey through Lingomon. These huts store a collection of all completed milestones.

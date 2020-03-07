# Test Project | Match Invaders 

*Test project for applicants*: Requires **Unity 2019.2.8f1**. 

## Goal 

Implement a small game based on two different game mechanics (Space Invaders and Match 3). 

- Don't spend more than 6-8 hours *for coding* on it. 
- Concentrate on gameplay, data structures, architecture and separation of concerns. If you desire, you can add custom assets, but the subject of evaluation will be the gameplay feel, the code and the implementation of the requirements. 
- Everything in the project is used for your reference, you can use any of the assets or completely remove all of them. 

## Mechanics 

### Player 

The player must drive a ship at the bottom of the screen that can move from right to left and shoot (with cooldown) up to eliminate enemy ships. 

It will have 3 lives that will be lost as enemy shots collide with the player. 

### Enemies 

The enemies will be arranged in the form of a grid at the top of the screen as shown in the figure. They will go down as in the Space Invaders game, from right to left going down one line each time they reach the right or left stop (see references on YouTube). The less ships remaining, the faster they will move. 

Enemies shoot at intervals but no more than 5 enemies can shoot at a time and only if they are the last in each column of enemies (that is, those closest to the player in each column). 

![example](Images/candy.png)

Each enemy ship will have a color (Blue, Red, Green or Yellow). When a ship of a specific color is eliminated all ships of the same color adjacent to its 4 sides will be destroyed as well. 

Each ship will award 10 points for its destruction, but if they are destroyed in chain their score will be increased using Fibonacci: 

```
1 ship = 1 * 10 = 10 points
2 ships = 2 * 20 = 40 points
3 ships = 3 * 30 = 90 points
4 ships = 4 * 50 = 90 points
5 ships = 5 * 80 = 400 points
N ships = N * Fibonacci(N+1) * 10 points 
```

### Level 

The level will have 4 protections that will be destroyed as the enemy shoots them. Each protector can absorb up to 5 shots before breaking. 

By eliminating all the enemies from one level, you will go to the next level, there is no change from level to level, you should only restart the enemies, the player position, the amount of lives of the player, and the protectors. 

### Menu 

The main menu will have at least the options to play and exit the game. Playing takes you to the first level and resets the score to 0 each time you lose. In the game, you can exit with the escape key to the start menu and start again. 

## Requirements 

- On the main menu, I should see the current highscore and when pressing play, change to the game scene. 
- Every time an enemy ship is destroyed, the score should increase. 
- The enemies color should be randomly picked. No two playthroughs should have the same pattern. 
- When losing, a menu showing me my last score, my current highscore and the buttons: "Restart" and "Go back to main menu" should appear. The buttons should take the user back to the home scene and restart the level. 
- When I press escape, the game should pause, and I should have a menu with the option "Continue" and "Go back to main menu". The buttons should either continue the game or take the user back to the home scene. 
- When I surpass my highscore, this should change in the UI. 
- The enemies speed should be inversely proportional to the amount left (the less enemies, the faster they are). 
- When I surpass my highscore, this should persist,  and I should be able to see it in the UIs when I launch the game again. 
- As a product owner, I want to be able to change the initial speed of the enemies, the speed of the bullets and the speed and amount of lives of the player  outside of Unity (json, xml, etc.). 
- Write [automated tests](https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html). 

### Bonus points 

- Have some smooth transition between level transitions (fading/effects/etc). 
- When surpassing my highscore, a new visual element should appear in the lost screen UI informing me of my achievement (like a text saying "You have a new highscore"). 
- When I'm about to surpass my highscore, a visual element should inform me of it (a different explosion color, a particle effect, etc.). 
- Don't use PlayerPrefs for persistence. 
- Add some details to make the game scene prettier (a color frame, screen shaking when dying, etc.). 
- When restarting the game, don't reload the scene. 

## Implementation 

- Clone this repository. 
- Develop as you would do in a normal project. 
- Do not use copyright/third party libraries. 
- Upload this project to a repository in your account and invite [Bullrich](http://github.com/bullrich/) to it. 

## Delivery 

### Before implementation 

- Split the project in sub tasks and estimate the time it would take for each one. You should use [GitHub issues](https://help.github.com/en/github/managing-your-work-on-github/creating-an-issue) for this. A template has been provided. 
- Inform  <javier.bullrich@innerspace.eu> about the estimations. 
- Optionally, contact Javier Bullrich for requirements clarification. 

### Implementation 

- Functional build for x64 machines. 

### After implementation 

- Comment the execution time for each issue closed. You can also add some extra information explaining why a task took less or more time than the estimated.  [You can also link it to a commit using a keyword](https://help.github.com/en/enterprise/2.16/user/github/managing-your-work-on-github/closing-issues-using-keywords). 
- Review the project (what you learn, where you struggle, what you liked from what you did). You can use the Review.md for this. 
- Tag the final commit as "Release" and upload the build it to the GitHub releases page. 
- Inform Javier Bullrich of the finished projects. 

## Evaluation criteria 

- Project completion (game and tests) 
- Structure of the project and the code 
- How and which software patterns you use. 
- Maintainability and cleanliness of your code. 
- How you test your code. 
- Creativity 
- Development time (effectiveness) 

--- 

Questions at any time to <javier.bullrich@innerspace.eu>
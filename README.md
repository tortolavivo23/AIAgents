# AIAgents

### Overview

**AIAgents** is a Unity-based project designed to demonstrate various AI movement behaviors and techniques. This project showcases three distinct types of AI agents with different behaviors: **Police Patrols**, **Bee Swarms**, and **Wandering Ghosts**. Each agent highlights unique AI patterns, from simple waypoint navigation to advanced machine learning-driven wandering.

You can view a demonstration of these agents in action via the [YouTube video](https://youtu.be/fbbAWub9AwA).

### AI Agents

#### 1. Police Patrols

- **Behavior**: Patrolling
- **Description**: The police patrols are groups of officers that move in coordinated formations. They follow a series of predefined waypoints, ensuring they patrol specific areas in a structured and predictable manner. This showcases basic pathfinding and coordinated group movement.

#### 2. Bee Swarms

- **Behavior**: Flocking and Pursuit
- **Description**: This agent demonstrates swarm behavior, where two groups of bees move cohesively as a single unit. The swarm exhibits a realistic flocking mechanism, ensuring smooth and coordinated movement. Additionally, the bees actively pursue the police patrols, adapting their movement to efficiently track and chase their targets. The behavior of the bees is controlled using **Unity's Behaviour Trees**, **Decision Trees**, and **State Machines**, allowing for structured and flexible decision-making and behavior management.

#### 3. Wandering Ghost

- **Behavior**: Wandering
- **Description**: The wandering ghost showcases a more complex AI agent that behaves unpredictably. Unlike the other agents, its movement is driven by Unity's **ML-Agents Toolkit**, which allows the ghost to learn and adapt through reinforcement learning. This results in organic, random movement patterns, offering a more dynamic and unpredictable behavior.

### Key Features

- **Patrolling**: Demonstrates structured, waypoint-based movement where agents follow predefined paths.
- **Flocking**: Realistic group dynamics that simulate natural swarm behaviors (e.g., bees moving together as a cohesive unit).
- **Pursuit**: Agents, such as the bees, are capable of dynamically adjusting their movement to track and follow specific targets.
- **Machine Learning**: Utilizes Unity's ML-Agents to enable self-learning and autonomous wandering behavior, producing unpredictable and adaptive agent actions.
- **Behaviour Trees**: Used for the bee agents to implement decision-making logic in a modular and scalable way.
- **Decision Trees and State Machines**: Employed for advanced agent behaviors, including movement and state transitions, allowing the agents to make more complex decisions based on different inputs and conditions.

### Getting Started

1. Clone or download the repository to your local machine.
2. Open the project in Unity.
3. Run the scene to see the AI agents in action.

### Requirements

- Unity (version 2020.3 or higher recommended).
- ML-Agents Toolkit (for the wandering ghost agent).

### Future Improvements

- Adding more complex behaviors such as avoidance or cooperative tasks.
- Expanding the use of state machines and decision trees to other agents for more dynamic behaviors.

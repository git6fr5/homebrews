using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour {

    public Mob mob;
    public Behaviour(Mob mob) {
        this.mob = mob;
    }

    public virtual void OnBehaviour() {

    }

    public virtual void WhileBehaviour() {

    }

    public virtual bool EndCondition() {
        return false;
    }


    /* --- Behaviours --- */
    public class Idle : Behaviour {

        public float interval;
        private float ticks = 0f;
        public Idle(Mob mob, float interval) : base(mob) {
            this.mob = mob;
            this.interval = interval;
        }

        public override void OnBehaviour() {
            // Reset the ticks
            ticks = 0f;
        }

        public override void WhileBehaviour() {
            mob.moveSpeed = 0f;
            mob.movementVector = Vector2.zero;
            ticks += Time.deltaTime;
        }

        public override bool EndCondition() {
            if (ticks >= interval) {
                return true;
            }
            return false;
        }

    }

    public class Movement : Behaviour, IDirection {

        public Movement(Mob mob) : base(mob) {
            this.mob = mob;
        }

        public override void OnBehaviour() {
            // Get the target point.
            int distance = GetDistance();
            Vector2 direction = GetDirection();
            mob.targetPoint = (Vector2)mob.transform.position + direction;
        }

        public override void WhileBehaviour() {
            mob.moveSpeed = mob.state.baseSpeed;
            mob.movementVector = (mob.targetPoint - (Vector2)mob.transform.position).normalized;
            mob.orientationVector = mob.movementVector;
        }

        public override bool EndCondition() {
            if (((Vector2)mob.transform.position - mob.targetPoint).magnitude < 0.05f) {
                mob.transform.position = mob.targetPoint;
                return true;
            }
            return false;
        }

        public virtual int GetDistance() {
            return 1;
        }

    }

    public class FixedDistance : Movement {

        public int distance;
        public FixedDistance(Mob mob, int distance) : base(mob) {
            this.mob = mob;
            this.distance = distance
        }

        public override int GetDistance() {
            return distance;
        }
    }

    public class RandomDistance : Movement {

        public int minDistance;
        public int maxDistance;
        public RandomDistance(Mob mob, int minDistance, int maxDistance) : base(mob) {
            this.mob = mob;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
        }

        public override int GetDistance() {
            return Random.Range(minDistance, maxDistance);
        }

    }

    interface IDirection {

        public override void GetDirection() {
            Vector2 direction = State.OrientationVectors[(State.Orientation)Random.Range(0, 4)];
        }

    }

    public class RangedCardinalMovement : Movement {

        public int distance => Random.Range(minDistance, maxDistance);
        public int minDistance;
        public int maxDistance;
        public RangedCardinalMovement(Mob mob, int minDistance, int maxDistance) {
            this.mob = mob;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
        }

        public override void OnBehaviour() {
            // Get the target point
            Vector2 direction = State.OrientationVectors[(State.Orientation)Random.Range(0, 4)];
            mob.targetPoint = (Vector2)mob.transform.position +  distance * direction;
        }

    }

    public class FixedDiagonalMovement : Movement {

        public int verticalDistance;
        public int horizontalDistance;
        public FixedDiagonalMovement(Mob mob, int verticalDistance, int horizontalDistance) {
            this.mob = mob;
            this.verticalDistance = verticalDistance;
            this.horizontalDistance = horizontalDistance;
        }

        public override void OnBehaviour() {
            // Get the target point
            int horizontalIndex = (2 * Random.Range(0, 2)) % (int)State.Orientation.count;
            int verticalIndex = (2 * Random.Range(0, 2) + 1) % (int)State.Orientation.count;
            Vector2 horizontalDirection = State.OrientationVectors[(State.Orientation)horizontalIndex];
            Vector2 verticalDirection = State.OrientationVectors[(State.Orientation)verticalIndex];
            mob.targetPoint = (Vector2)mob.transform.position + horizontalDistance * horizontalDirection + verticalDistance * verticalDirection;
        }

    }

}


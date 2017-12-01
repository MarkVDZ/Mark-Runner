using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the AABB setup so objects can collide
/// </summary>
public class AABB : MonoBehaviour {

    /// <summary>
    /// Stores the half size of an object for calculating edges
    /// </summary>
    public Vector3 halfSize;

    /// <summary>
    /// The min and the max of the AABB
    /// </summary>
    Vector3 min = Vector3.zero;
    Vector3 max = Vector3.zero;

    /// <summary>
    /// This is the offset used for objects that pivot isn't at their center
    /// </summary>
    public Vector3 offset;
	
    /// <summary>
    /// Update is called every frame
    /// </summary>
	void Update () {
        calcEdges();
	}
    /// <summary>
    /// Calculates the edges of objects for AABB using min and max
    /// </summary>
    public void calcEdges()
    {
        min = transform.position - halfSize;
        max = transform.position + halfSize;
    }
    /// <summary>
    /// Checks for collison with other AABB objects
    /// </summary>
    /// <param name="other">Passes in another AABB object to check against</param>
    /// <returns>If there isn't a gap, there is collison</returns>
    public bool checkOverlap(AABB other)
    {
        if(other != null)
        {
            if (min.x > other.max.x) return false;
            if (max.x < other.min.x) return false;

            if (min.y > other.max.y) return false;
            if (max.y < other.min.y) return false;

            if (min.z > other.max.z) return false;
            if (max.z < other.min.z) return false;

            return true;
        }
        return true;
    }

    //How far to move this AABB to correct its overlap with other AABB
    /// <summary>
    /// This method is called when there is an overlap and moves one of the objects which ever way
    /// is the shortest distance to move
    /// </summary>
    /// <param name="other">Passes in another AABB object to check against</param>
    /// <returns>Returns the vector 3 of the move</returns>
    public Vector3 CalculateOverlapFix(AABB other)
    {
        
        float moveRight = other.max.x - min.x;
        float moveUp = other.max.y - min.y;
        float moveForward = other.max.z - min.z;

        float moveLeft = other.min.x - max.x;
        float moveDown = other.min.y - max.y;
        float moveBack = other.min.z - max.z;

        Vector3 solution;
    
        solution.z = Mathf.Abs(moveForward) < Mathf.Abs(moveBack) ? moveForward : moveBack;
        solution.y = Mathf.Abs(moveUp) < Mathf.Abs(moveDown) ? moveUp : moveDown;
        solution.x = Mathf.Abs(moveRight) < Mathf.Abs(moveLeft) ? moveRight : moveLeft;

        if (Mathf.Abs(solution.x) < Mathf.Abs(solution.z) && Mathf.Abs(solution.x) < Mathf.Abs(solution.y))
        {
            solution.z = 0;
            solution.y = 0;

        }

        if (Mathf.Abs(solution.y) < Mathf.Abs(solution.x) && Mathf.Abs(solution.y) < Mathf.Abs(solution.z))
        {
            solution.x = 0;
            solution.z = 0;

        }
        
        if (Mathf.Abs(solution.z) < Mathf.Abs(solution.x) && Mathf.Abs(solution.z) < Mathf.Abs(solution.y))
        {
            solution.x = 0;
            solution.y = 0;

        }
        //if (Mathf.Abs(solution.z) > Mathf.Abs(solution.x) || Mathf.Abs(solution.z) > Mathf.Abs(solution.y)) solution.z = 0;
        //if (Mathf.Abs(solution.y) > Mathf.Abs(solution.x) || Mathf.Abs(solution.y) > Mathf.Abs(solution.z)) solution.y = 0;
        //if (Mathf.Abs(solution.x) > Mathf.Abs(solution.y) || Mathf.Abs(solution.x) > Mathf.Abs(solution.z)) solution.x = 0;

        return solution;

    }
}

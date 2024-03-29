﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomRoom
{
    public class RoomNode : MonoBehaviour
    {
        [SerializeField] RoomManager roomManager;

        public List<AttachPoint> AttachPoints;
        [HideInInspector] public List<AttachPoint> randomPoints;

        public GameObject RoomContent;
        public GameObject RoomArtwork;

        public List<RoomCollisionPoint> CollisionPoints;
        [SerializeField] private float collisionTestRadius = 4f;

        #region RoomSetup

        private void Awake()
        {
            randomPoints = AttachPoints;
            ShuffleList(randomPoints);

            foreach (var point in CollisionPoints)
            {
                point.RoomManager = roomManager;
                point.Room = this;
            }

            foreach (var point in AttachPoints)
                point.RoomManager = roomManager;
        }

        /// <summary>
        /// Randomizes list, so it can be looped over randomly without repeat.
        /// </summary>
        /// <param name="objectList">List to randomize</param>
        private static void ShuffleList(List<AttachPoint> objectList)
        {
            for (var i = 0; i < objectList.Count; i++)
            {
                var random = Random.Range(i, objectList.Count);
                (objectList[random], objectList[i]) = (objectList[i], objectList[random]);
            }
        }

        /// <summary>
        /// Sets up room, goes through each attach point in room find good orientation.
        /// </summary>
        /// <param name="attachPoint"> Point room is being attached to</param>
        /// <returns>True on success, false of failure</returns>
        public bool SetupRoom(Transform attachPoint)
        {
            for (var i = 0; i < randomPoints.Count; i++)
            {
                OrientRoom(attachPoint, randomPoints[i].transform);

                if (!RoomColliding())
                {
                    attachPoint.gameObject.SetActive(false);
                    randomPoints[i].gameObject.SetActive(false);
                    randomPoints.RemoveAt(i);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Rotates room to fit against attach point.
        /// </summary>
        /// <param name="attachPoint">Point to attach to</param>
        /// <param name="newPoint">Point of room being rotated</param>
        private void OrientRoom(Transform attachPoint, Transform newPoint)
        {
            var pointRotation = Quaternion.FromToRotation(newPoint.forward, -attachPoint.forward);

            transform.rotation = pointRotation;

            if (transform.rotation.eulerAngles.z == 180)
                transform.rotation = Quaternion.Euler(0, 180, 0);

            var offset = transform.position - newPoint.position;
            transform.position = attachPoint.position + offset;
        }

        /// <summary>
        /// Test if room is colliding
        /// </summary>
        /// <returns>True on collision, false on no collision</returns>
        private bool RoomColliding()
        {
            return CollisionPoints.Any(position =>
                Physics.OverlapSphere(position.transform.position, collisionTestRadius).Length > 0);
        }

        #endregion

        /// <summary>
        /// Create rooms for each unused point in room.
        /// </summary>
        public void ExtendRoom()
        {
            for (var i = randomPoints.Count - 1; i >= 0; i--)
            {
                roomManager.CreateRoom(randomPoints[i].transform);
                randomPoints.RemoveAt(i);
            }
        }
    }
}

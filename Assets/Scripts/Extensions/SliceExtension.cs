using System.Collections.Generic;
using EzySlice;
using UnityEngine;

namespace Extensions
{
    public static class SliceExtension
    {
        public static GameObject ShatterObject(GameObject obj, int stage, Material crossSectionMaterial = null)
        {
            var position = obj.transform.position;
            return obj.Slice(new Vector3(position.x,
                obj.transform.lossyScale.y / stage,
                position.z), obj.transform.up, crossSectionMaterial).CreateLowerHull(obj);
        }

        public static List<GameObject> InitSlice(GameObject gameObjectPrefab, Transform parentTransform,  Vector3 spawnOffset, int sliceCount)
        {
            var hulls = new List<GameObject>
            {
                Object.Instantiate(gameObjectPrefab, parentTransform.position + spawnOffset, Quaternion.identity, parentTransform)
            };

            for (var i = 1; i < sliceCount + 1; i++)
            {
                var obj = ShatterObject(hulls[0], i);
                obj.transform.position = parentTransform.position + spawnOffset;
                obj.transform.parent = parentTransform;
                hulls.Add(obj);
            }

            return hulls;
        }
    }
}
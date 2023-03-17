using System.Collections.Generic;
using System.Linq;
using Garden;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private readonly List<Crop> _crops = new List<Crop>();
        public bool HasCrops => _crops.Any(x => x.IsExtractable);
        public Crop GetLastCrop => _crops.FirstOrDefault(x=>x.IsExtractable);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Crop crop))
            {
                _crops.Add(crop);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Crop crop))
            {
                _crops.Remove(crop);
            }
        }
    }
}
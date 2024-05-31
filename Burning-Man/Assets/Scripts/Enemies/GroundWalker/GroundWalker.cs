using UnityEngine;

namespace  BurningMan
{
    public class GroundWalker : MonoBehaviour
    {
        public Transform target;
        public float orbitSpeed;
        public float OrbitDistance;
        public GameObject ParentObject;
        private float _orbitSpeed;

        private float angle;
        void Start()
        {
            int RandomNumb = Random.Range(-3,4);
            ParentObject.transform.position = new (RandomNumb,ParentObject.transform.position.y);
            _orbitSpeed = Random.Range(orbitSpeed-2, orbitSpeed+4);
        }

        void Update()
        {
            angle += _orbitSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * OrbitDistance;
            Vector2 newPosition = new Vector2(target.position.x, target.position.y) + offset;
            transform.position = newPosition;

            Vector2 direction = (target.position - transform.position).normalized;
            float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;  // -90 derece ekliyoruz çünkü sprite default olarak yukarı bakıyorsa
            transform.rotation = Quaternion.Euler(0, 0, angleToTarget);
        }
    }
}



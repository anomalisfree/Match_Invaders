using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        [UnityTest]
        public IEnumerator EnemyMove()
        {
            var enemy =
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enemy")).GetComponent<EnemyItem>();
            enemy.Initialize(1, 1, 10, 10);

            var initialPos = enemy.transform.position;

            enemy.SetPos(2, 1);
            yield return new WaitForSeconds(2);
            Assert.Greater(enemy.transform.position.x, initialPos.x);

            enemy.SetPos(0, 1);
            yield return new WaitForSeconds(2);
            Assert.Less(enemy.transform.position.x, initialPos.x);

            enemy.SetPos(1, 2);
            yield return new WaitForSeconds(2);
            Assert.Less(enemy.transform.position.y, initialPos.y);

            enemy.SetPos(1, 0);
            yield return new WaitForSeconds(2);
            Assert.Greater(enemy.transform.position.y, initialPos.y);

            Object.Destroy(enemy.gameObject);
        }

        [UnityTest]
        public IEnumerator EnemyShoot()
        {
            var enemy =
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enemy")).GetComponent<EnemyItem>();
            enemy.Initialize(0, 0, 10, 10);
            enemy.Shoot();

            yield return null;

            Assert.NotNull(GameObject.Find("BulletEnemy(Clone)"));
            Object.Destroy(enemy.gameObject);
        }

        [UnityTest]
        public IEnumerator EnemyDead()
        {
            var enemy =
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enemy")).GetComponent<EnemyItem>();
            enemy.Initialize(0, 0, 10, 10);
            enemy.DelayDead();

            yield return null;
            yield return null;

            Assert.Null(GameObject.Find("Enemy"));
        }
    }
}
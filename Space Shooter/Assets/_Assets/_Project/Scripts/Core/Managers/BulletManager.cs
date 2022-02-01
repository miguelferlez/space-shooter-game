using Core.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers
{
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance;
        private Dictionary<Bullet, List<Bullet>> bulletDic = new Dictionary<Bullet, List<Bullet>>();

        public void Awake()
        {
            Instance = this;
        }

        public Bullet GetBullet(Bullet bulletRef)
        {
            if (!bulletDic.ContainsKey(bulletRef))
            {
                CreatePool(bulletRef);
            }

            var list = bulletDic[bulletRef];
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].enabled)
                {
                    list[i].Init();
                    return list[i];
                }
            }

            var bull = CreateBullet(bulletRef);
            list.Add(bull);
            bull.Init();
            return bull;
        }

        public void CreatePool(Bullet bulletModel)
        {
            List<Bullet> bullets = new List<Bullet>();
            for (int i = 0; i < 10; i++)
            {
                bullets.Add(CreateBullet(bulletModel));
            }

            bulletDic.Add(bulletModel, bullets);
        }

        private Bullet CreateBullet(Bullet bulletModel)
        {
            var bull = Instantiate(bulletModel);
            bull.Finish();
            return bull;
        }
    }
}
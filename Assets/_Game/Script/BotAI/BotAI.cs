using System.Collections.Generic;
using UnityEngine;

public class BotAI : CharacterController
{
    private GameObject target; // Biến để lưu trữ mục tiêu hiện tại của bot

    private void Start()
    {
        // Random target khi khởi động game
        SetTarget();
    }

    private void Update()
    {
        // Kiểm tra xem bot đã đạt được mục tiêu hay chưa
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            // Nếu target trong range tấn công, bot sẽ dừng lại để bắn
            if (distance < attackRange)
            {
                transform.LookAt(target.transform);

                // Tấn công target
                Attack();
            }
            else
            {
                // Di chuyển tới mục tiêu
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Nếu không có target thì chọn target mới
            SetTarget();
        }
    }

    private void SetTarget()
    {
        // Lấy danh sách các character từ CharacterSpawner
        List<GameObject> characters = CharacterSpawner.instance.GetCharacters();

        // Chọn ngẫu nhiên một character từ danh sách
        if (characters.Count > 0)
        {
            int randomIndex = Random.Range(0, characters.Count);
            target = characters[randomIndex];
        }
    }

    private void Attack()
    {
        // Thực hiện hành động tấn công tại đây
    }
}

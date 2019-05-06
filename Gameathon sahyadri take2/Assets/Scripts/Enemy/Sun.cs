using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : BaseEnemyScript {

    [Header("Barrel Info")]
    public int barrel_subdivisions;
    public int barrel_per_division;
    public Transform[] barrels;
    Transform[,] all_barrels;

    [Header("Laser info")]
    public float laser_shoot_timer = 2f;
    public float laser_max_length = 4f;
    public Gradient hit_color;
    public Gradient no_hit_color;

    [Header("alternate lazer info")]
    public float next_clk_delay = 3f;
    int clk = 0;
    private bool can_shoot = true;

    public enum sun_state { idle, shoot}
    sun_state current_state;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();

        all_barrels = new Transform[barrel_subdivisions, barrel_per_division];
        for (int i = 0; i < barrel_subdivisions; i++)
        {
            for (int j = 0; j < barrel_per_division; j++)
            {
                all_barrels[i, j] = barrels[i * barrel_per_division + j];
            }
        }
    }

    public override void Update()
    {
        base.Update();

        switch (current_state) {
            case sun_state.idle:
                if (can_shoot && base.isActive)
                {
                    StartCoroutine(changeSunState());
                    can_shoot = false;
                    StartCoroutine(nextClock());
                }
                break;
            case sun_state.shoot:
                for (int i = 0; i < barrel_per_division; i++)
                {
                    shootLaser(all_barrels[clk, i]);
                }
                //shooting
                break;
        }
        //if (base.isActive)
        //{
        //    anim.SetBool("is_active", true);
        //}
        //else
        //{
        //    anim.SetBool("is_active", false);
        //}
    }

    IEnumerator nextClock()
    {
        yield return new WaitForSeconds(next_clk_delay);
        clk++;
        if (clk >= barrel_subdivisions)
            clk = 0;
        can_shoot = true;
    }
    IEnumerator changeSunState()
    {
        current_state = sun_state.shoot;
        yield return new WaitForSeconds(laser_shoot_timer);
        for (int i = 0; i < barrel_per_division; i++)       // before going to idle state, remove all the existing lasers
        {
            withdrawLaser(all_barrels[clk, i]);
        }
        current_state = sun_state.idle;
    }

    void shootLaser(Transform barrel)
    {
        // mask ray to only platforms, player, enemy and bullets. So take bitwise or of each of those layers and left shift
        int layer_mask = 1 << 8 | 1 << 9 | 1 << 10 | 1 << 11 | 1 << 12;
        RaycastHit2D hit = Physics2D.Raycast(barrel.position, barrel.right, laser_max_length, layer_mask);      //cast a ray from barrel position, right direction
        barrel.GetComponent<LineRenderer>().SetPosition(0, barrel.position);
        if (hit.collider != null)
        {
            Transform obstruction = hit.collider.transform;
            Debug.DrawLine(barrel.position, hit.point, Color.red);
            barrel.GetComponent<LineRenderer>().SetPosition(1, hit.point);
            barrel.GetComponent<LineRenderer>().colorGradient = no_hit_color;
            //if collision object is player, then reduce health
            if (obstruction.CompareTag("Player"))
            {
                obstruction.GetComponent<Health>().reduceHealth(1);
                barrel.GetComponent<LineRenderer>().colorGradient = hit_color;
            }
        }
        else
        {
            Debug.DrawLine(barrel.position, barrel.position + barrel.right * laser_max_length, Color.green);
            //else draw line of laser_max_length in forward direction
            barrel.GetComponent<LineRenderer>().SetPosition(1, barrel.position + barrel.right * laser_max_length);
            barrel.GetComponent<LineRenderer>().colorGradient = no_hit_color;
        }
    }

    void withdrawLaser(Transform barrel)
    {
        barrel.GetComponent<LineRenderer>().SetPositions(new Vector3[] { barrel.position, barrel.position });
    }
	
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Panel : MonoBehaviour
{
   [SerializeField] private SpriteRenderer spriteRenderer = default;
   [SerializeField] private Collider2D thisCollider = default;

   [SerializeField] private Color hideColor = default;
   private void OnTriggerEnter2D(Collider2D collider)
   {
       var player = collider.GetComponent<PlayerController>();
       player.AddPanel(this);
   }

   private void OnTriggerExit2D(Collider2D collider)
   {
       var player = collider.GetComponent<PlayerController>();
       player.RemovePanel(this);
       Hide();
   }

   public void Hide()
   {
       thisCollider.enabled = false;
       spriteRenderer.color = hideColor;
   }
   public void Show()
   {
       thisCollider.enabled = true;
       spriteRenderer.color = Color.white;
   }
}

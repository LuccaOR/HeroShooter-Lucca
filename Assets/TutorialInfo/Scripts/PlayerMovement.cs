using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed = 5f;
    public Rigidbody rb;
    private Vector3 direction;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("Dash")]
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;

    void Update()
    {
        // Se estiver executando o Dash, ignora o controle normal de movimento/pulo
        if (isDashing) return;

        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        direction = Vector3.zero;

        if (Keyboard.current[Key.W].isPressed)
        {
            direction += transform.forward;
        }
        if (Keyboard.current[Key.S].isPressed)
        {
            direction -= transform.forward;
        }
        if (Keyboard.current[Key.D].isPressed)
        {
            direction += transform.right;
        }
        if (Keyboard.current[Key.A].isPressed)
        {
            direction -= transform.right;
        }

        direction = Vector3.ClampMagnitude(direction, 1f);

        // Verifica se está no chão via Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Ação de Pulo (Barra de Espaço)
        if (Keyboard.current[Key.Space].wasPressedThisFrame && isGrounded)
        {
            Jump();
        }

        // Ação de Dash (Shift Esquerdo)
        if (Keyboard.current[Key.LeftShift].wasPressedThisFrame && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);
    }

    private void Jump()
    {
        // Reseta a velocidade vertical para garantir pulos consistentes
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;

        // Se o jogador estiver parado, dá o dash para a frente do personagem
        Vector3 dashDir = direction.sqrMagnitude > 0.01f ? direction.normalized : transform.forward;

        // Mantém a velocidade Y atual para o dash não cancelar a queda/pulo de forma estranha
        rb.linearVelocity = new Vector3(dashDir.x * dashForce, rb.linearVelocity.y, dashDir.z * dashForce);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // Cooldown antes de poder usar o dash novamente
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Linha visual para ajustar o alcance da verificação do chão no Inspector
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
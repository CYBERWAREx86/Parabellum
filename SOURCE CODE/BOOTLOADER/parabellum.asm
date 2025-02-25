[BITS 16]
[ORG 0x7C00]

WSCREEN equ 320
HSCREEN equ 200

start:
    mov ax, 0x13
    int 0x10
    mov ax, 0xA000
    mov es, ax

main_loop:
    call draw_waves
    call delay
    jmp main_loop

draw_waves:
    xor di, di
    mov cx, WSCREEN 
    mov dx, HSCREEN  
    mov bx, [frame]   

draw_loop:
    mov ax, di
    add ax, bx  
    and ax, 0xFF        
    add al, dl        
    shr al, 3
    

    mov ah, bl
    and ah, 0x90    
    
    cmp ah, 0
    je set_red
    cmp ah, 20
    je set_blue

set_blue:
    and al, 0x8F
    add al, 252
    jmp apply_color

set_green:
    and al, 0x2F
    add al, 22
    jmp apply_color

set_red:
    and al, 0x2F
    add al, 13

apply_color:
    mov [es:di], al

    inc di 
    loop draw_loop

    mov cx, WSCREEN
    dec dx
    jnz draw_loop

    inc word [frame]
    ret


delay:
    mov cx, 0xFFFF
    mov dx, 0x0115

delay_outer_loop:
    mov cx, 0xFFFF

delay_inner_loop:
    nop
    loop delay_inner_loop
    dec dx
    jnz delay_outer_loop
    ret


frame dw 1
times 510 - ($ - $$) db 0
dw 0xAA55
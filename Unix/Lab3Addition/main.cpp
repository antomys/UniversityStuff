#include <sys/ptrace.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>
#include <sys/user.h>
#include <sys/reg.h>
#include <sys/syscall.h>   /* For SYS_write etc */

#include <string.h>
#include <stdlib.h>
#include <stdio.h>

const int long_size = sizeof(long);
void reverse(char *str)
{
    int i, j;
    char temp;
    for(i = 0, j = strlen(str) - 2;
        i <= j; ++i, --j){
        temp = str[i];
        str[i] = str[j];
        str[j] = temp;
    }
}

void getdata(pid_t child, long addr,
             char *str, int len)
{
    int i,j = 0;
    long data = 0;
    char *tmp = str;
    for ( i=0, j=0; i<len; i+=long_size, j++){
        data = ptrace(PTRACE_PEEKDATA,
                      child,
                      addr + (j * long_size),
                      NULL);
        memcpy((void*)tmp, (void*)&data, long_size);
        tmp = tmp + long_size;
    }

    str[len] = '\0';
}

void putdata(pid_t child, long addr,
             char *str, int len)
{
    int i,j = 0;
    char *tmp = str;
    for ( i=0, j=0; i<len; i+=long_size, j++){
        long val;
        memcpy(&val, tmp, long_size);
        ptrace(PTRACE_POKEDATA,
               child,
               addr + (j * long_size),
               val);
        tmp = tmp + long_size;
    }
}

int main()
{
    pid_t child;
    long orig_rax;
    long params[3];
    int status;
    int toggle = 0;
    char *str = 0;
    child = fork();
    if(child == 0) {
        ptrace(PTRACE_TRACEME, 0, NULL, NULL);
        execl("/bin/ls", "ls ", NULL);
    }
    else {
        while(1){
            wait(&status);
            if(WIFEXITED(status))
                break;
            orig_rax = ptrace(PTRACE_PEEKUSER,
                              child,
                              long_size * ORIG_RAX,
                              NULL);
            if(orig_rax == SYS_write){
                toggle = 1;
                params[0] = ptrace(PTRACE_PEEKUSER,
                                   child,
                                   long_size * RDI,
                                   NULL);
                params[1] = ptrace(PTRACE_PEEKUSER,
                                   child,
                                   long_size * RSI,
                                   NULL);
                params[2] = ptrace(PTRACE_PEEKUSER,
                                   child,
                                   long_size * RDX,
                                   NULL);
                int malloc_len = params[2] + params[2] % long_size + 1;
                str = (char *)malloc(malloc_len);

                getdata(child, params[1], str, params[2]);
                reverse(str);
                putdata(child, params[1], str, params[2]);
            }
            ptrace(PTRACE_SYSCALL,
                   child,
                   NULL,
                   NULL);
        }
    }
    return 0;
}

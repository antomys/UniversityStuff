create or replace function random_string (
    str_length integer, lang varchar(2) default 'en',
    w_dig boolean default true, w_punct boolean default false,
    w_space boolean default false, w_newline boolean default false
)

returns text
language plpgsql
as $function$
     declare
         chars_eng text[] := '{A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z}';
         chars_rus text[] := '{А,Б,В,Г,Д,Е,Ё,Ж,З,И,Й,К,Л,М,Н,О,П,Р,С,Т,У,Ф,Х,Ц,Ч,Ш,Щ,Ъ,Ы,Ь,Э,Ю,Я,а,б,в,г,д,е,ё,ж,з,и,й,к,л,м,н,о,п,р,с,т,у,ф,х,ц,ч,ш,щ,ъ,ы,ь,э,ю,я}';
         chars_dig text[] := '{}';
         chars_punct text[] := '{}';
         chars_space text[] := '{}';
         chars_newline text[] := '{}';
         chars_final text[] := '{}';
         result text := '';
         i integer := 0;
    begin

        -- checking string length arg
        if str_length < 0 then
            raise exception 'Length of string cannot be a negative value';
        end if;

        -- checking chars selection
        if w_dig = true then
            chars_dig := '{0,1,2,3,4,5,6,7,8,9}';
        end if;
        if w_punct = true then
            chars_punct := string_to_array(E'!d"d#d$d%d&d\'d(d)d*d+d,d-d.d/d:d;d<d=d>d?d@d[d\\d]d^d_d`d{d|d}d~','d');
        end if;
        if w_space = true then
            chars_space := string_to_array(' ',',');
        end if;
        if w_newline = true then
            chars_newline := string_to_array(E'\r\n',',');
        end if;

        -- checking language selection
        if lang = 'en' then
            chars_final := chars_eng||chars_dig||chars_punct||chars_space||chars_newline;
        elsif lang = 'ru' then
            chars_final := chars_rus||chars_dig||chars_punct||chars_space||chars_newline;
        else
            raise exception 'Characters set for that language is not defined';
        end if;

        -- filling the string
        for i in 1..str_length loop
            result := result || chars_final[1 + round(random() * (array_length(chars_final, 1) - 1))];
        end loop;

        -- trimming extra symbols that may appear from /r/n usage
        if length(result) > str_length then
            result := left(result, str_length);
        end if;

        -- getting the result
        return result;

    end;
$function$ ;
main :-
  introduction,
  reset_answers,
  find_car(Car),
  describe(Car), nl.


introduction :-
  write('What kind of car do I want?'), nl,
  write('To answer, input the number shown next to each answer, followed by a dot (.)'), nl, nl.


find_car(Car) :-
  car(Car), !.

:- dynamic(progress/2).

reset_answers :-
  retract(progress(_, _)),
  fail.
reset_answers.

% Rules
car(lada_priora) :-
	why(urgent),
	smt_exotic(no),
	soviet(yes),
	speedy(not_bad),
	comfort(not_bad).

car(zaz968m) :-
	why(urgent),
	comfort(im_a_fan),
	speedy(love).

car(crappy_car) :-
	smt_exotic(yes),
	soviet(yes),
	speedy(not_bad),
	comfort(love).

car(veg) :-
	comfort(hate),
	smt_old(no).

car(rastaman_car) :-
	why(for_fun),
	smt_exotic(yes),
	smt_old(yes),
	comfort(love),
	soviet(yes).

car(cheese_bomb) :-
	luxury(im_a_fan).

car(devil_car) :-
	why(for_fun),
	soviet(yes),
	speedy(im_a_fan),
	comfort(love).

car(lada2108) :-
	why(for_fun),
	smt_exotic(yes),
	smt_old(yes).

% Questions
question(fav_ing) :-
	write('What is your favorite car spec?'), nl.

question(why) :-
	write('Why do you want a car?'), nl.

question(luxury) :-
	write('How much do you love luxury?'), nl.

question(smt_old) :-
	write('Do you want something old?'), nl.

question(smt_exotic) :-
	write('Do you love something exotic?'), nl.

question(soviet) :-
	write('Do you love soviet cars?'), nl.

question(speedy) :-
	write('How much do you love speedy car ?'), nl.

question(comfort) :-
	write('How much do you love comfortable car?'), nl.

% Answers
answer(for_fun) :-
	write('For fun').

answer(i_dont_know) :-
	write('I don\'t know').

answer(urgent) :-
	write('I urgently need it!').

answer(hate) :-
	write('Hate').

answer(love) :-
	write('Love').

answer(yes) :-
  write('Yes').

answer(no) :-
	write('No').

answer(im_a_fan) :-
  write('I\'m a fan!').

answer(not_bad) :-
	write('Not Bad').

answer(comfort) :-
	write('Comfort').

answer(veg) :-
	write('Vegetables').

answer(soviet) :-
	write('Soviet').

answer(luxury) :-
	write('Luxury').

% Descriptions
describe(lada_priora) :-
  write('Lada Priora'), nl,
  write('Eto lada priora. Baklazhan vmesto motora'), nl.

describe(cheese_bomb) :-
  write('Car Old crap'), nl,
  write('Barely moving'), nl.

describe(crappy_car) :-
  write('Car Bombastic'), nl,
  write('Too good to be true.'), nl.

describe(veg) :-
  write('Vegetarian car '), nl,
  write('Moves on green energy.'), nl.

describe(rastaman_car) :-
  write('Rastaman car'), nl,
  write('Moves on smoke.'), nl.

describe(lada2108) :-
  write('Lada 2108'), nl,
  write('Good car for a good man'), nl.

describe(devil_car) :-
  write('Devil car '), nl,
  write('Only for cool people'), nl.

describe(zaz968m) :-
  write('The Ultimate car Zaz 968M'), nl,
  write('Best of the best'), nl.

% Assigns an answer to questions 
why(Answer) :-
  progress(why, Answer).
why(Answer) :-
  \+ progress(why, _),
  ask(why, Answer, [for_fun, i_dont_know, urgent]).

fav_ing(Answer) :-
  progress(fav_ing, Answer).
fav_ing(Answer) :-
  \+ progress(fav_ing, _),
  ask(fav_ing, Answer, [soviet,i_dont_know, luxury,veg,comfort]).

luxury(Answer) :-
  progress(luxury, Answer).
luxury(Answer) :-
  \+ progress(luxury, _),
  ask(luxury, Answer, [hate,love,not_bad,im_a_fan]).

soviet(Answer) :-
  progress(soviet, Answer).
soviet(Answer) :-
  \+ progress(soviet, _),
  ask(smt_old, Answer, [yes,no]).

comfort(Answer) :-
  progress(comfort, Answer).
comfort(Answer) :-
  \+ progress(comfort, _),
  ask(comfort, Answer, [hate,love,not_bad,im_a_fan]).

speedy(Answer) :-
  progress(speedy, Answer).
speedy(Answer) :-
  \+ progress(speedy, _),
  ask(speedy, Answer, [hate,love,not_bad]).

smt_exotic(Answer) :-
  progress(smt_exotic, Answer).
smt_exotic(Answer) :-
  \+ progress(smt_exotic, _),
  ask(smt_exotic, Answer, [yes,no]).

smt_old(Answer) :-
  progress(smt_old, Answer).
smt_old(Answer) :-
  \+ progress(smt_old, _),
  ask(smt_old, Answer, [yes,no]).

answers([], _).
answers([First|Rest], Index) :-
  write(Index), write(' '), answer(First), nl,
  NextIndex is Index + 1,
  answers(Rest, NextIndex).


parse(0, [First|_], First).
parse(Index, [First|Rest], Response) :-
  Index > 0,
  NextIndex is Index - 1,
  parse(NextIndex, Rest, Response).



ask(Question, Answer, Choices) :-
  question(Question),
  answers(Choices, 0),
  read(Index),
  parse(Index, Choices, Response),
  asserta(progress(Question, Response)),
Response = Answer.

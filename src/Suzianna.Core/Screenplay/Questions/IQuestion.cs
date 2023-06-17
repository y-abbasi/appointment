﻿using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay.Questions;

public interface IQuestion<out TAnswer>
{
    TAnswer AnsweredBy(Actor actor);
}
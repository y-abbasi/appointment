using System.Collections.Generic;
using System.Linq;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay.Questions;

internal class SumQuestion : IQuestion<long>
{
    private readonly List<IQuestion<long>> _questions;

    public SumQuestion(List<IQuestion<long>> questions)
    {
        _questions = questions;
    }

    public long AnsweredBy(Actor actor)
    {
        return _questions.Sum(question => question.AnsweredBy(actor));
    }
}
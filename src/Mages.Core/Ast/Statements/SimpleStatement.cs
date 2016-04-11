﻿namespace Mages.Core.Ast.Statements
{
    sealed class SimpleStatement : BaseStatement, IStatement
    {
        #region Fields

        private readonly IExpression _expression;

        #endregion

        #region ctor

        public SimpleStatement(IExpression expression, TextPosition end)
            : base(expression.Start, end)
        {
            _expression = expression;
        }

        #endregion

        #region Methods

        public void Validate(IValidationContext context)
        {
            _expression.Validate(context);
        }

        #endregion
    }
}

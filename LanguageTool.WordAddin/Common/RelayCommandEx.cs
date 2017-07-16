﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LanguageTool.WordAddin.Common
{
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// Backing field for the execute action.
        /// </summary>
        private readonly Action<T> execute;

        /// <summary>
        /// Backing field for the can execute check function.
        /// </summary>
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<T> execute) : this(execute, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Raised when RaiseCanExecuteChanged is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether this <see cref="RelayCommandEx"/> can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute((T)parameter);
        }

        /// <summary>
        /// Executes the <see cref="RelayCommandEx"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }

        /// <summary>
        /// Method used to raise the <see cref="CanExecuteChanged"/> event
        /// to indicate that the return value of the <see cref="CanExecute"/>
        /// method has changed.
        /// </summary>
        public void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
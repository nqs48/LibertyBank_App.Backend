﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Helpers.ObjectsUtils.ResponseObjects
{
    /// <summary>
    /// ResponseError
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ResponseError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseError"/> class.
        /// </summary>
        public ResponseError()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseError"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public ResponseError(List<ResponseContent> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<ResponseContent> Errors { get; set; }
    }
}
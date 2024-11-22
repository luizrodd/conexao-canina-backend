﻿using ConexaoCaninaApp.Domain.Models;

namespace ConexaoCaninaApp.Application.Dto
{
    public class SugestaoDTO
    {
        public Guid SugestaoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEnvio { get; set; }
        public SugestaoStatus Status { get; set; }
        public string Feedback { get; set; }
    }
}

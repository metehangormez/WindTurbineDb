using Microsoft.AspNetCore.Mvc;
using WindTurbine.DataAccess.Context;
using WindTurbine.Entities;
using WindTurbine.DTOs.Reports; // DTO namespace'ini eklemeyi unutmayın
using System;
using System.Linq;

namespace WindTurbineDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly WindTurbineDbContext _context;

        public ReportsController(WindTurbineDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reports = _context.GeneratedReports
                .OrderByDescending(r => r.GeneratedDate)
                .Select(r => new ReportDto
                {
                    ReportId = r.Id,
                    Title = r.Title,
                    GeneratedDate = r.GeneratedDate,
                    FileType = r.FileType,
                    FileSize = r.FileSize,
                    Status = "Hazır"
                })
                .ToList();
            return Ok(reports);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReportDto dto)
        {
            var report = new GeneratedReport
            {
                Title = dto.Title,
                GeneratedDate = DateTime.UtcNow,
                FileType = dto.FileType,
                FileSize = dto.FileSize,

                // --- HATAYI ÇÖZEN SATIR BURASI ---
                // Veritabanı bu alanın NULL olmasına izin vermediği için
                // boş bir byte dizisi (byte array) atıyoruz.
                Content = new byte[0]
                // --------------------------------
            };

            _context.GeneratedReports.Add(report);
            _context.SaveChanges();

            // Oluşan ID'yi geri dönmek iyi bir pratiktir
            dto.ReportId = report.Id;
            return Ok(dto);
        }
    }
}
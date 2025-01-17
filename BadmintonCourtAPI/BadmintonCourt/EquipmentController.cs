using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/equipment")]
public class EquipmentController : ControllerBase
{
    private static List<Equipment> Equipments = new()
    {
        new Equipment { Id = 1, Name = "Racket A", IsBorrowed = false },
        new Equipment { Id = 2, Name = "Shuttlecock Set", IsBorrowed = true, BorrowedBy = "John", BorrowedDate = DateTime.UtcNow.AddDays(-1) }
    };

    // GET: ดึงรายการอุปกรณ์ทั้งหมด
    [HttpGet]
    public IActionResult GetAllEquipments()
    {
        return Ok(Equipments);
    }

    // GET: ดึงอุปกรณ์เฉพาะ ID
    [HttpGet("{id}")]
    public IActionResult GetEquipmentById(int id)
    {
        var equipment = Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null)
            return NotFound("Equipment not found!");

        return Ok(equipment);
    }

    // POST: เพิ่มอุปกรณ์ใหม่
    [HttpPost]
    public IActionResult AddEquipment([FromBody] Equipment newEquipment)
    {
        newEquipment.Id = Equipments.Count + 1;
        newEquipment.IsBorrowed = false; // อุปกรณ์ใหม่ยังไม่มีการยืม
        Equipments.Add(newEquipment);
        return Ok("Equipment added successfully!");
    }

    [HttpPut("{id}/borrow")]
    public IActionResult BorrowEquipment(int id, [FromBody] string borrowedBy)
    {
        var equipment = Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null)
            return NotFound("Equipment not found!");

        if (equipment.IsBorrowed)
            return BadRequest("Equipment is already borrowed!");

        equipment.IsBorrowed = true;
        equipment.BorrowedBy = borrowedBy;
        equipment.BorrowedDate = DateTime.UtcNow;
        return Ok("Equipment borrowed successfully!");
    }

    [HttpPut("{id}/return")]
    public IActionResult ReturnEquipment(int id)
    {
        var equipment = Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null)
            return NotFound("Equipment not found!");

        if (!equipment.IsBorrowed)
            return BadRequest("Equipment is not currently borrowed!");

        equipment.IsBorrowed = false;
        equipment.BorrowedBy = null;
        equipment.BorrowedDate = null;
        return Ok("Equipment returned successfully!");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEquipment(int id)
    {
        var equipment = Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null)
            return NotFound("Equipment not found!");

        Equipments.Remove(equipment);
        return Ok("Equipment deleted successfully!");
    }
}

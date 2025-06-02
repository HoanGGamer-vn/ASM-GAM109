using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using System;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public string calculate_rank(int score)
    {
        if (score >= 1000)
            return "Diamond";
        else if (score >= 500)
            return "Gold";
        else if (score >= 100)
            return "Silver";
        else
            return "Bronze";
    }

    public void YC1(int ID, string Name, int Score, Region IDRegion)
    {
        Players newPlayer = new Players(ID, Name, Score, IDRegion);
        listPlayer.Add(newPlayer);
    }

    public void YC2()
    {
        foreach (Players player in listPlayer)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log($"Tên: {player.Name} | ID: {player.Id} | Điểm: {player.Score}| Rank: {rank} | Vùng: {player.Region.Name}");
        }
    }

    public void YC3()
    {
        int currentScore = ScoreKeeper.Instance.GetScore();
        foreach (Players player in listPlayer)
        {
            if (player.Score < currentScore)
            {
                string rank = calculate_rank(player.Score);
                Debug.Log($"[PLAYER] Tên: {player.Name} | ID: {player.Id} | Điểm: {player.Score} | Vùng: {player.Region.Name} | Rank: {rank}");
            }
        }
    }

    public void YC4()
    {
        int currentId = ScoreKeeper.Instance.GetID();
        Players player = listPlayer.FirstOrDefault(p => p.Id == currentId);
        if (player != null)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log($"[KẾT THÚC MÀN CHƠI] Tên: {player.Name} | ID: {player.Id} | Điểm: {player.Score} | Vùng: {player.Region.Name} | Rank: {rank}");
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy người chơi với ID: {currentId}");
        }
    }

    public void YC5()
    {
        var sortedPlayers = listPlayer.OrderByDescending(p => p.Score);

        Debug.Log("[DANH SÁCH NGƯỜI CHƠI THEO ĐIỂM GIẢM DẦN]");

        foreach (Players player in sortedPlayers)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log($"Tên: {player.Name} | ID: {player.Id} | Điểm: {player.Score} | Vùng: {player.Region.Name} | Rank: {rank}");
        }
    }

    public void YC6()
    {
        var lowest5Players = listPlayer
            .OrderBy(p => p.Score)
            .Take(5);

        Debug.Log("[TOP 5 NGƯỜI CHƠI CÓ ĐIỂM THẤP NHẤT]");

        foreach (Players player in lowest5Players)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log($"Tên: {player.Name} | ID: {player.Id} | Điểm: {player.Score} | Vùng: {player.Region.Name} | Rank: {rank}");
        }
    }
    public void YC7()
    {
        Thread thread = new Thread(() =>
        {
            var regionScores = listPlayer
                .GroupBy(p => p.Region)
                .Select(g => new
                {
                    RegionName = g.Key.Name,
                    AverageScore = g.Average(p => p.Score)
                })
                .OrderByDescending(r => r.AverageScore);

            string path = Path.Combine(Application.persistentDataPath, "bxhRegion.txt");

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine("[BẢNG XẾP HẠNG ĐIỂM TRUNG BÌNH THEO VÙNG]");
                foreach (var region in regionScores)
                {
                    writer.WriteLine($"Vùng: {region.RegionName} | Điểm trung bình: {region.AverageScore:F2}");
                }
            }

            Debug.Log($"[BXH] Đã ghi xong bảng xếp hạng vào tập tin: {path}");
        });

        thread.Name = "BXH";
        thread.Start();
    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        var regionScores = listPlayer.GroupBy(p => p.Region).Select(g => new
        {
            RegionName = g.Key.Name,
            AverageScore = g.Average(p => p.Score)
        })
        .OrderByDescending(r => r.AverageScore);
        string path = Path.Combine(Application.persistentDataPath, "bxhRegion.txt");
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.WriteLine("[BẢNG XẾP HẠNG ĐIỂM TRUNG BÌNH THEO VÙNG]");
            foreach (var region in regionScores)
            {
                writer.WriteLine($"Vùng: {region.RegionName} | Điểm trung bình: {region.AverageScore:F2}");
            }
        }
        Debug.Log($"[BXH] Đã ghi xong bảng xếp hạng vào tập tin: {path}");
    }

}

[SerializeField]
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    public int Id;
    public string Name;
    public int Score;
    public Region Region;

    public Players(int id, string name, int score, Region region)
    {
        this.Id = id;
        this.Name = name;
        this.Score = score;
        this.Region = region;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Score: {Score}, Region: {Region.Name}"; 
    }
}
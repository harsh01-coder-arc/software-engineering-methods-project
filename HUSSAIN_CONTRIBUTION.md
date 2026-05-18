
I handled **Reports 11 through 20**, which covered two main themes:
- Ranking capital cities by population (globally, by continent, and by region)
- Breaking down how many people live inside cities versus outside them

---

## Files I Added
| File | Location |
|------|----------|
| `HussainReports11to20.sql` | `ReportApp/` |

---

## Reports I Completed

### Capital City Rankings

| Report | Description |
|--------|-------------|
| Report 11 | All capital cities worldwide, largest to smallest population |
| Report 12 | All capital cities in a chosen continent, largest to smallest |
| Report 13 | All capital cities in a chosen region, largest to smallest |
| Report 14 | Top N most populated capitals in the world |
| Report 15 | Top N most populated capitals in a chosen continent |
| Report 16 | Top N most populated capitals in a chosen region |

### Population Breakdowns (City vs Non-City)

| Report | Description |
|--------|-------------|
| Report 17 | Urban vs rural split across every continent |
| Report 18 | Urban vs rural split across every region |
| Report 19 | Urban vs rural split for one selected continent |
| Report 20 | Urban vs rural split for one selected region |

---

## How I Tested the Queries
All queries run against the `country` and `city` tables in the **World database**.

Each query was checked to make sure it:
- ✅ Orders results correctly by population (largest first)
- ✅ Filters accurately by continent when required
- ✅ Filters accurately by region when required
- ✅ Calculates city population totals correctly
- ✅ Calculates non-city population figures correctly
- ✅ Produces accurate percentage breakdowns for both groups

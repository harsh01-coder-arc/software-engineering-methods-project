# Use Cases - Population Reporting System

## USE CASE 1: View Top N Countries by Population

**Use Case ID:** UC-01  
**Use Case Name:** View Top N Most Populated Countries Worldwide  
**Actors:** Data Analyst, User  

**Preconditions:**
- System is running
- Database connection is active
- User has access to the application

**Main Flow:**
1. User launches the application
2. User selects "Top Countries" report
3. System prompts user to enter value for N (number of countries)
4. User enters N (e.g., 10)
5. System queries the database for top N countries sorted by population
6. System displays results in ranked order (largest to smallest)
7. User views country names, populations, and rankings

**Postconditions:**
- Top N countries are displayed correctly
- Data is sorted by population in descending order

**Alternative Flow:**
- If user enters invalid N (negative or zero), system shows error message
- If database connection fails, system shows error and allows retry

---

## USE CASE 2: View Top N Cities by Population

**Use Case ID:** UC-02  
**Use Case Name:** View Top N Most Populated Cities Worldwide  
**Actors:** City Planner, Resource Manager, User  

**Preconditions:**
- System is running
- Database connection is active

**Main Flow:**
1. User launches application
2. User selects "Top Cities" report
3. System prompts for N (number of cities)
4. User enters N (e.g., 20)
5. System queries database for top N cities by population
6. System displays results: City name, Country, Population, Ranking
7. User views and analyzes city data

**Postconditions:**
- Top N cities displayed in descending order by population
- Results include country information for context

**Error Handling:**
- Invalid input triggers validation error
- Database errors show user-friendly message

---

## USE CASE 3: View Countries by Continent

**Use Case ID:** UC-03  
**Use Case Name:** View All Countries in a Specific Continent  
**Actors:** Data Analyst, User  

**Preconditions:**
- System is operational
- User is authenticated
- World database is accessible

**Main Flow:**
1. User opens application
2. User selects "Countries by Continent" report
3. System displays list of continents (Africa, Asia, Europe, etc.)
4. User selects a continent (e.g., Asia)
5. System retrieves all countries in that continent from database
6. System displays countries sorted by population (largest first)
7. Results show: Country Name, Region, Population, Capital
8. User analyzes continental data

**Postconditions:**
- All countries in selected continent are displayed
- Data is accurately filtered and sorted

**Alternative Flow:**
- If continent has no countries, system displays message
- User can select different continent and repeat process

---

## USE CASE 4: View Language Statistics

**Use Case ID:** UC-04  
**Use Case Name:** View Global Language Speaker Statistics  
**Actors:** User, Data Analyst, Researcher  

**Preconditions:**
- System is running
- Database contains language data
- Connection to world database established

**Main Flow:**
1. User launches application
2. User selects "Language Statistics" report
3. System displays language options (English, Spanish, Mandarin, etc.)
4. User selects one or more languages
5. System queries database for number of speakers worldwide
6. System calculates and displays:
   - Total speakers per language
   - Percentage of world population
   - Regions where language is spoken
7. Results are displayed in table format sorted by number of speakers

**Postconditions:**
- Language statistics are accurate
- Data shows global distribution of speakers

**Error Handling:**
- If language data unavailable, show appropriate message
- Invalid selections prevented by UI dropdown
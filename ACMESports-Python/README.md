# FastAPI Project


This project marks my initial venture into the world of Python and FastAPI. Embarking on this journey has been immensely exciting. As with all first steps, I recognize there's a vast ocean of best practices and techniques I can still learn to refine and enhance the quality of my Python code. 

## Getting Started

### Prerequisites

- Python 3.9+
- FastAPI
- Docker
- Docker-Compose

### Installation & Setup

1. **Clone the repository**:
    ```bash
    git clone https://github.com/ryanrichard19/ACMESports
    cd ACMESports-Python
    ```

2. **Install the required packages**:
    ```bash
    pip install -r requirements.txt
    ```

### Running the API

To run the API, use the following command:

```bash
uvicorn app.main:app --reload
```

### Running Unit Tests

    ```bash
    pytest tests/unit_tests
```
### Running Integration Tests


1. **Boot up Prism using Docker-Compose**:
    ```bash
    docker-compose up
```
2. **Verify Prism is running**:

   Once Prism is up and running, you can access its mocked endpoints from your API tests or via tools like `curl` or Postman, typically at `http://0.0.0.0:4010`.

3. **Run your integration tests**
    ```bash
    pytest tests/integration_tests
```

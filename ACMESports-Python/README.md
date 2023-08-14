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

### 4. Testing Latency

To simulate network latency in your Docker container, you can use the `tc` (traffic control) tool with the `netem` (network emulator) module. Here are the commands to add, change, and remove latency:

**Add latency:**
```bash
docker exec <CONTAINER_ID_OR_NAME> tc qdisc add dev eth0 root netem delay 200ms
```
**Increase latency:**
```bash
docker exec <CONTAINER_ID_OR_NAME> tc qdisc change dev eth0 root netem delay 200ms
```
**Remove latency:**
```bash
docker exec <CONTAINER_ID_OR_NAME> tc qdisc del dev eth0 root netem
```

#include <bits/stdc++.h>

using namespace std;

class Solution {
public:
template <typename T>
    using min_pq = std::priority_queue<T, std::vector<T>, std::greater<T>>;
    using ll = long long;
    using pli = pair<ll, int>;

    int mostBooked(int n, vector<vector<int>>& meetings) {
      min_pq<int> roomsPq;
      for(int i = 0; i < n; i++) {
        roomsPq.emplace(i);
      }
      
      sort(meetings.begin(), meetings.end());

      vector<int> counter(n);

      min_pq<pli> runningMeetingPq;
      for(auto &meeting : meetings) {
        while(!runningMeetingPq.empty() && runningMeetingPq.top().first <= meeting[0]) {
          auto room = runningMeetingPq.top().second;
          runningMeetingPq.pop();

          roomsPq.emplace(room);
        }

        ll startsIn = meeting[0];
        int size = meeting[1] - meeting[0];
        int room;

        if(roomsPq.empty()) {
          auto [runningMeetingEndsIn, runningMeetingRoom] = runningMeetingPq.top();
          runningMeetingPq.pop();

          room = runningMeetingRoom;
          startsIn = runningMeetingEndsIn;
        } else {
          room = roomsPq.top();
          roomsPq.pop();
        }

        runningMeetingPq.emplace(startsIn + size, room);
        counter[room]++;
      }

      int mostUsedRoom = 0;
      for(int i = 1; i < n; i++) {
        if(counter[i] > counter[mostUsedRoom]) {
          mostUsedRoom = i;
        }
      }

      return mostUsedRoom;
    }
};
